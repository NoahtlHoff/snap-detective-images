namespace SnapDetective.Images.Models;

public class Image
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public required string FileName { get; set; }
    public List<string> Answers { get; set; } = [];
    public int ImageSetId { get; set; }
    public ImageSet ImageSet { get; set; } = null!;
}
