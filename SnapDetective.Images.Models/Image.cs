using System;
using System.Collections.Generic;
using System.Text;

namespace SnapDetective.Images.Models;

public class Image
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int ImageSetId { get; set; }
    public ImageSet ImageSet { get; set; } = null!;
}
