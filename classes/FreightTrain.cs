using System;

namespace sma_prep;

public class FreightTrain : Train
{
    public double LoadTonnes { get; set; }

    public FreightTrain()
    {
        MaxSpeedKmh = 100;
    }
}