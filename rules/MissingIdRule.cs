using System;
using System.Reflection;
using sma_prep.interfaces;

namespace sma_prep.rules;

public class MissingIdRule : ITrainRule
{
    public IEnumerable<ValidationMessage> Check(Train train)
    {
        if (string.IsNullOrEmpty(train.Id))
        {
            yield return new ValidationMessage(null, Severity.ERROR, "Train missing Id!");
        } 
    }
}
