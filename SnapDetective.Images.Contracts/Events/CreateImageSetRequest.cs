using System;
using System.Collections.Generic;
using System.Text;

namespace SnapDetective.Images.Contracts.Events;

public record CreateImageSetRequest(string Name, string? Description);
