using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Azure.Messaging.ServiceBus;
using SnapDetective.Images.Interfaces;
using SnapDetective.Images.Repository;
using SnapDetective.Images.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
}
else
{
    builder.Services.AddSingleton(_ =>
        new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus")));
    builder.Services.AddScoped<IMessagePublisher, AzureServiceBusPublisher>();
}

builder.Services.AddSingleton(_ =>
    new BlobServiceClient(builder.Configuration.GetConnectionString("BlobStorage")));

builder.Services.AddScoped<IImageSetRepository, ImageSetRepository>();
builder.Services.AddScoped<IImageSetService, ImageSetService>();
builder.Services.AddScoped<IMessagePublisher, RabbitMqPublisher>();
builder.Services.AddScoped<IBlobStorageService, AzureBlobStorageService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();