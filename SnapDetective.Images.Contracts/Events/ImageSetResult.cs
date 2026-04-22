using System;
using System.Collections.Generic;
using System.Text;

namespace SnapDetective.Images.Contracts;

public record ImageSetResult(int Id, string Name, string? Description, List<ImageResult> Images);
public record ImageResult(int Id, string Url, string Answer);
