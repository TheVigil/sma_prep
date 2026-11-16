using System;

namespace sma_prep;

public class SectionOccupation
{
    public string SectionId { get; set; } = "";
    public TimeOnly Entry { get; set; }
    public TimeOnly Exit { get; set; }
}
