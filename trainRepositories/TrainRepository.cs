using System;
using System.Text.Json;
using sma_prep.trains;

namespace sma_prep.trainRepositories;

public class TrainRepository : ITrainRepository
{
    private readonly string _path;
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true};

    public TrainRepository(string path) => _path = path;

    public async Task<IReadOnlyList<Train>> GetAllTrainsAsync(CancellationToken cancellationToken = default)
    {
        await using var stream = File.OpenRead(_path);
        
        var trains = await JsonSerializer.DeserializeAsync<List<Train>>(stream, options, cancellationToken);

        return trains ?? new List<Train>();
    }
}
