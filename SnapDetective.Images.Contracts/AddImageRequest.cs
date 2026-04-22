using System.Collections.Generic;

namespace SnapDetective.Images.Contracts;

public record AddImageRequest(string FileName, string ContentType, Stream FileStream, List<string> Answers);