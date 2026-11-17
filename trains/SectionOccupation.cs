using System;

namespace sma_prep.trains;

public record SectionOccupation(string SectionId, TimeOnly Entry, TimeOnly Exit);
