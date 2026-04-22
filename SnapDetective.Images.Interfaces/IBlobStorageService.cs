namespace SnapDetective.Images.Interfaces;
public interface IBlobStorageService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
    Task DeleteAsync(string fileName);
}
