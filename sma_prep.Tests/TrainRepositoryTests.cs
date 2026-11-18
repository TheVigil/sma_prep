using System;
using sma_prep.trainRepositories;
using sma_prep.trains;

namespace sma_prep.Tests;

public class TrainRepositoryTests
{
    [Fact]
    public async Task Train_Repository_Loads_Data_Async()
    {
        #region test json
        var testJson = """
                        [{
                            "$type": "Passenger",
                            "id": "TestPassenger",
                            "operator": "SBB",
                            "maxSpeedKmh": 160,
                            "sections": [
                            { "sectionId": "ZH-OE", "entry": "08:00:00", "exit": "08:06:00" }
                            ]
                        },
                        {
                            "$type": "Freight",
                            "id": "TestFreight",
                            "operator": "SBB Cargo",
                            "loadTonnes": 1800,
                            "maxSpeedKmh": 100,
                            "sections": [
                            { "sectionId": "OE-WI", "entry": "08:20:00", "exit": "08:35:00" }
                            ]
                        }]
                        """;
        #endregion
        
        var tmpPath = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tmpPath, testJson);

            TrainRepository repository = new(tmpPath);
            var trains = await repository.GetAllTrainsAsync();


            // repository should have two listed trains
            Assert.Equal(2, trains.Count);

            //the first train is a Passenger Train
            Assert.IsType<Train>(trains[0]);

            // the second train is a FreightTrain with a load tonnage of 1800
            var freightTrain = Assert.IsType<FreightTrain>(trains[1]);
            Assert.Equal(1800, freightTrain.LoadTonnes);
        }
        finally
        {
            File.Delete(tmpPath);
        }

    }

    [Fact]
    public async Task GetAllTrainsAsync_Repository_DoesNotExist_Throws_Exception()
    {
            // an error is thrown if the repository is non existent
            var pathToBadFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json"); // guaranteed path to a nonexistent file
            TrainRepository trainRepository = new(pathToBadFile);
            
            await Assert.ThrowsAsync<FileNotFoundException>(() => trainRepository.GetAllTrainsAsync());
    }
}
