namespace SnapDetective.Images.Models;

public class Image
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public List<string> Answers { get; set; } = [];
    public int ImageSetId { get; set; }
    public ImageSet ImageSet { get; set; } = null!;
}
