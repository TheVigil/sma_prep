using System;
using System.Text.Json;

namespace sma_prep.classes;

public class TrainRepository : ITrainRepository
{
    private readonly string _path;
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true};

    public TrainRepository(string path) => _path = path;

    public IReadOnlyList<Train> GetTrains()
    {
        var json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<Train>>(json, options) ?? new List<Train>();
    }
}
