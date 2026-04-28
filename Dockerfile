FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG AZURE_ARTIFACTS_PAT

WORKDIR /src

COPY nuget.config ./
RUN if [ -n "$AZURE_ARTIFACTS_PAT" ]; then \
      dotnet nuget update source snap-detective \
        --username "docker" \
        --password "$AZURE_ARTIFACTS_PAT" \
        --store-password-in-clear-text \
        --configfile nuget.config; \
    fi

COPY SnapDetective.Images.Contracts/SnapDetective.Images.Contracts.csproj         SnapDetective.Images.Contracts/
COPY SnapDetective.Images.Models/SnapDetective.Images.Models.csproj               SnapDetective.Images.Models/
COPY SnapDetective.Images.Interfaces/SnapDetective.Images.Interfaces.csproj       SnapDetective.Images.Interfaces/
COPY SnapDetective.Images.Repository/SnapDetective.Images.Repository.csproj       SnapDetective.Images.Repository/
COPY SnapDetective.Images.Services/SnapDetective.Images.Services.csproj           SnapDetective.Images.Services/
COPY SnapDetective.Images.Api/SnapDetective.Images.Api.csproj                     SnapDetective.Images.Api/

RUN dotnet restore "SnapDetective.Images.Api/SnapDetective.Images.Api.csproj"

COPY . .
WORKDIR /src/SnapDetective.Images.Api
RUN dotnet build "SnapDetective.Images.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SnapDetective.Images.Api.csproj" \
      -c $BUILD_CONFIGURATION \
      -o /app/publish \
      /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SnapDetective.Images.Api.dll"]