using System.Collections.Generic;

namespace SnapDetective.Images.Contracts;

public record ImageSetResult(int Id, string Name, string? Description, List<ImageResult> Images);
public record ImageResult(int Id, string Url, List<string> Answers);
