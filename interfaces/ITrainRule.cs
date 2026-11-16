using System;

namespace sma_prep.interfaces;

public interface ITrainRule
{
    IEnumerable<ValidationMessage> Check(Train train);
    
}
