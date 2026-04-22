using System;
using System.Collections.Generic;
using System.Text;

namespace SnapDetective.Images.Contracts;

public record AddImageRequest(string FileName, string ContentType, Stream FileStream, string Answer);