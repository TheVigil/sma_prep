using System;

namespace sma_prep.trains;

public class FreightTrain : Train
{
    public double LoadTonnes { get; set; }

    public FreightTrain()
    {
        MaxSpeedKmh = 100;
    }
}