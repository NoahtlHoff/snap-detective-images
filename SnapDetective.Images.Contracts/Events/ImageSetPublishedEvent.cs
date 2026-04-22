using System;
using System.Collections.Generic;
using System.Text;

namespace SnapDetective.Images.Contracts.Events;

public record ImageSetPublishedEvent(int ImageSetId, string Name, List<ImageEventItem> Images);
public record ImageEventItem(int ImageId, string Url, string Answer);