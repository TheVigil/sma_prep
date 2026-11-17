using System;
using sma_prep.trains;

namespace sma_prep.policies;

public class SpeedRestriction
{
    public void ApplyRestriction(IReadOnlyList<Train> trains, double restriction)
    {
        foreach(var train in trains)
        {
            train.MaxSpeedKmh = Math.Min(train.MaxSpeedKmh, restriction);
        }
    }
}
