namespace SnapDetective.Images.Models;

public class ImageSet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Image> Images { get; set; } = [];
}
