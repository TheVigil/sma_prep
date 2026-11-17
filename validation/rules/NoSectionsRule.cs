using System;
using sma_prep.rules;
using sma_prep.trains;

namespace sma_prep.rules;

public class NoSectionsRule: ITrainRule
{
    public IEnumerable<ValidationMessage> Check(Train train)
    {
        if(train.Sections.Count == 0)
        {
            yield return new ValidationMessage(train.Id, Severity.WARNING, $"Train {train.Id} has no sections");
        }
    }
}
