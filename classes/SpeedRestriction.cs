using System;

namespace sma_prep.classes;

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
