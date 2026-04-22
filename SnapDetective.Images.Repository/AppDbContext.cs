using Microsoft.EntityFrameworkCore;
using SnapDetective.Images.Models;

namespace SnapDetective.Images.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ImageSet> ImageSets => Set<ImageSet>();
    public DbSet<Image> Images => Set<Image>();
}