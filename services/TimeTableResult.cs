using System;
using sma_prep.rules;
using sma_prep.trains;

namespace sma_prep.services;

public record TimeTableResult(
    IReadOnlyList<ValidationMessage> ValidationMessages,
    IReadOnlyList<Conflict> Conflicts
);