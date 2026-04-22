using SnapDetective.Images.Models;

namespace SnapDetective.Images.Interfaces;

public interface IImageSetRepository
{
    Task<List<ImageSet>> GetAllAsync();
    Task<ImageSet?> GetByIdAsync(int id);
    Task<ImageSet> CreateAsync(ImageSet imageSet);
    Task AddImageAsync(int imageSetId, Image image);
}