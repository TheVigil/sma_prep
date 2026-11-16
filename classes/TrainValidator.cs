using System;
using System.Data;
using sma_prep.interfaces;

namespace sma_prep;

public enum Severity { INFO, WARNING, ERROR };
public record ValidationMessage(string? TrainId, Severity Severity, string Message);

public class TrainValidator
{
    private readonly IEnumerable<ITrainRule> _rules;
    public TrainValidator(IEnumerable<ITrainRule> rules) => _rules = rules;    

    public IReadOnlyList<ValidationMessage> ValidateTrains(List<Train> trains)
    {
        var validationMessages = new List<ValidationMessage>();

        foreach(var train in trains)
        {
            foreach(var rule in _rules)
            {
                validationMessages.AddRange(rule.Check(train));
            }
        }
        return validationMessages;
    }
}
