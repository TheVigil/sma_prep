using System;
using System.Net.Http.Headers;
using sma_prep.policies;
using sma_prep.trains;

namespace sma_prep.Tests;

public class ConflictDetectorTests
{
    private static Train Passenger(string id, string section, int entryMin, int exitMin) => new() 
    {
        Id = id,
        Sections = {new SectionOccupation(section, new TimeOnly(8, entryMin), new TimeOnly(8, exitMin))}
    };

    private static Train Freight(string id, string section, int entryMin, int exitMin) => new FreightTrain()
    {
        Id = id,
        Sections = {new SectionOccupation(section, new TimeOnly(8, entryMin), new TimeOnly(8, exitMin))}
    };

    private readonly ConflictDetector _conflictDetector = new(new StandardHeadwayPolicy(freightHeadwayMinutes: 5, minHeadwayMinutes: 3));

    [Fact]
    public void PassengerTrains_FourMinutesApart_NoConflict()
    {
        var trains = new []{Passenger("A", "S1", 0, 6), Passenger("B", "S1", 10, 16)};

        var conflicts = _conflictDetector.DetectConflicts(trains);

        Assert.Empty(conflicts);
    }

    [Fact]
    public void Trains_Overlap_Section_At_Same_Time_Conflict()
    {
        var trains = new[] { Passenger("A", "S1", 0, 6), Passenger("B", "S1", 4, 10) };

        var conflicts = _conflictDetector.DetectConflicts(trains);

        var conflict = Assert.Single(conflicts);
        Assert.Equal("A", conflict.TrainIdA);
        Assert.Equal("B", conflict.TrainIdB);
        Assert.Equal(3, conflict.HeadwayMinutes);
    }

    [Fact]
    public void Trains_Overlap_Section_At_Offset_Times_Conflict()
    {
        var trains = new []{Passenger("A", "S1", 0, 10), Passenger("B", "S1", 5, 15)};

        var conflicts = _conflictDetector.DetectConflicts(trains);

        var conflict = Assert.Single(conflicts);
        Assert.Equal("A", conflict.TrainIdA);
        Assert.Equal("B", conflict.TrainIdB);
        Assert.Equal(3, conflict.HeadwayMinutes);
    }

    [Fact]
    public void Different_Sections_No_Conflict()
    {
        var trains = new []{Passenger("A", "S1", 0, 10), Passenger("B", "S2", 5, 15)};

        var conflicts = _conflictDetector.DetectConflicts(trains);

        Assert.Empty(conflicts);
    }

    [Theory]
    [InlineData(8, true)]
    [InlineData(9, false)]
    [InlineData(10, false)]
    public void Section_Entry_At_Boundry_No_Conflict(int entryB, bool expectConflict)
    {
        var trains = new []{Passenger("A", "S1", 0, 6), Passenger("B", "S1", entryB, entryB + 6)};
        var conflicts = _conflictDetector.DetectConflicts(trains);

        Assert.Equal(expectConflict, conflicts.Count == 1);

    }

    [Fact]
    public void Passenger_Freight_Four_Minutes_Section_Headway_Conflict()
    {
        var trains = new []{Passenger("A", "S1", 0, 6), Freight("B", "S1", 10, 16)};
        var conflicts = _conflictDetector.DetectConflicts(trains);

        var conflict = Assert.Single(conflicts);
        Assert.Equal("A", conflict.TrainIdA);
        Assert.Equal("B", conflict.TrainIdB);
        Assert.Equal(5, conflict.HeadwayMinutes);
    }


}
