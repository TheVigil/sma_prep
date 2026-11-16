using System;

namespace sma_prep;

public interface ITrainRepository
{
    public IReadOnlyList<Train> GetTrains();
}
