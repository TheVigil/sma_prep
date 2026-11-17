using System;
using sma_prep.trains;

namespace sma_prep.policies;

public interface IHeadwayPolicy
{
    public int GetHeadwayMinutes(Train a, Train b);
}
