using System;
using sma_prep.rules;
using sma_prep.trains;

namespace sma_prep.rules;

public class FreightLoadLimitRule : ITrainRule
{
    private readonly double _maxLoadTonnes;

    public FreightLoadLimitRule(double maxLoadTonnes)
    {
        _maxLoadTonnes = maxLoadTonnes;
    }

    public IEnumerable<ValidationMessage> Check(Train train)
    {
        if(train is FreightTrain freightTrain && freightTrain.LoadTonnes > _maxLoadTonnes)
        {
            yield return new ValidationMessage(train.Id, Severity.ERROR, $"Train {train.Id} exceeds the {_maxLoadTonnes} weight limit");
        }
    }
}
