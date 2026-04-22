using Microsoft.EntityFrameworkCore;
using SnapDetective.Images.Interfaces;
using SnapDetective.Images.Models;

namespace SnapDetective.Images.Repository;

public class ImageSetRepository(AppDbContext db) : IImageSetRepository
{
    public Task<List<ImageSet>> GetAllAsync() =>
        db.ImageSets.Include(s => s.Images).ToListAsync();

    public Task<ImageSet?> GetByIdAsync(int id) =>
        db.ImageSets.Include(s => s.Images).FirstOrDefaultAsync(s => s.Id == id);

    public async Task<ImageSet> CreateAsync(ImageSet imageSet)
    {
        db.ImageSets.Add(imageSet);
        await db.SaveChangesAsync();
        return imageSet;
    }

    public async Task AddImageAsync(int imageSetId, Image image)
    {
        image.ImageSetId = imageSetId;
        db.Images.Add(image);
        await db.SaveChangesAsync();
    }
}