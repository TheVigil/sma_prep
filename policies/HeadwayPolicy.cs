using sma_prep.rules;
using sma_prep.trains;

namespace sma_prep.policies;

public class StandardHeadwayPolicy: IHeadwayPolicy
{

    private readonly int _freightHeadwayMinutes;
    private readonly int _minHeadwayMinutes;

    public StandardHeadwayPolicy(int freightHeadwayMinutes, int minHeadwayMinutes)
    {
        _freightHeadwayMinutes = freightHeadwayMinutes;
        _minHeadwayMinutes = minHeadwayMinutes;
    }

    public int GetHeadwayMinutes(Train a, Train b)
    {
        
        return (a is FreightTrain || b is FreightTrain) ? _freightHeadwayMinutes : _minHeadwayMinutes; 
    }
}
