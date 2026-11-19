using System.Runtime.CompilerServices;
using sma_prep.policies;
using sma_prep.rules;
using sma_prep.trains;
using TimetableBasics;

namespace sma_prep.Tests;

public class TimeTableServiceTest
{
    private static Train Passenger(string id, int entryMin, int exitMin) => new()
    {
        Id = id,
        Sections = {new SectionOccupation("S1", new TimeOnly(8, entryMin), new TimeOnly (8, exitMin))}
    };

    private static TimetableService TimetableTestService(params Train[] trains) => new (
        new FakeTrainRepository(trains), 
        new TrainValidator(new ITrainRule[]{new MissingIdRule(), new NoSectionsRule(), new FreightLoadLimitRule(1800)}),
        new ConflictDetector(new StandardHeadwayPolicy(freightHeadwayMinutes: 5, minHeadwayMinutes: 3))
    );

    [Fact]
    public async Task RunAsync_Two_Overlaps_Return_One_Conflict_No_Messages()
    {
        var result = await TimetableTestService(new[] {Passenger("A", 0, 6), Passenger("B", 3, 9)}).RunAsync(); 
        
        Assert.Single(result.Conflicts);
        Assert.Empty(result.ValidationMessages);

    }

    [Fact]
    public async Task RunAsync_Train_Missing_Info_Returns_No_Conflict_And_One_Message()
    {
        var result = await TimetableTestService(new[] {Passenger("A", 0, 6), new Train {Id = "B" /*train missing section info*/}}).RunAsync();

        Assert.Single(result.ValidationMessages);
        Assert.Empty(result.Conflicts);
    }
}
