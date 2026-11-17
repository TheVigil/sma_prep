using System;

namespace sma_prep.conflicts;

public class SectionOccupation
{
    public string SectionId { get; set; } = "";
    public TimeOnly Entry { get; set; }
    public TimeOnly Exit { get; set; }
}
