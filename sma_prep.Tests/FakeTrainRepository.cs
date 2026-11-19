using System;
using sma_prep.trainRepositories;
using sma_prep.trains;

namespace sma_prep.Tests;

public class FakeTrainRepository: ITrainRepository
{   
    private readonly IReadOnlyList<Train> _fakeRepository;

    public FakeTrainRepository(IReadOnlyList<Train> fakeRepository) => _fakeRepository = fakeRepository;
    
    public Task<IReadOnlyList<Train>> GetAllTrainsAsync(CancellationToken cancellationToken = default) => Task.FromResult(_fakeRepository);
} 
