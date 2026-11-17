using sma_prep.trains;

namespace sma_prep.rules;

public interface ITrainRule
{
    IEnumerable<ValidationMessage> Check(Train train);
    
}
