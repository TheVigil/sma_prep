using System;
using sma_prep.trains;

namespace sma_prep.trainRepositories;

public interface ITrainRepository
{
    public IReadOnlyList<Train> GetTrains();
}
