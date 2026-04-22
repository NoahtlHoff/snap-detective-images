using SnapDetective.Images.Contracts;

namespace SnapDetective.Images.Interfaces;

public interface IImageSetService
{
    Task<List<ImageSetResult>> GetAllAsync();
    Task<ImageSetResult?> GetByIdAsync(int id);
    Task<ImageSetResult> CreateAsync(CreateImageSetRequest request);
    Task AddImageAsync(int imageSetId, AddImageRequest request);
}
