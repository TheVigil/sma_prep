namespace sma_prep;

public record Conflict ( 
    string TrainIdA, SectionOccupation OccupationA, 
    string TrainIdB, SectionOccupation OccupationB,
    int headway
);

public class ConflictDetector(int minHeadwayMinutes, int freightHeadwayMinutes)
{
    private readonly int _minHeadwayMinutes = minHeadwayMinutes;
    private readonly int _freightHeadwayMinutes = freightHeadwayMinutes;
    public IReadOnlyList<Conflict> DetectConflicts(IReadOnlyList<Train> trains)
    {
        var conflicts = new List<Conflict>();
        for (int i = 0; i < trains.Count; i++)
        {
            for (int j = i + 1; j < trains.Count; j++)
            {
                var a = trains[i];
                var b = trains[j];
                foreach (var sa in a.Sections)
                {
                    foreach (var sb in b.Sections)
                    {
                        if (sa.SectionId != sb.SectionId)
                            continue;

                        int headway = (a is FreightTrain || b is FreightTrain)
                        ? _freightHeadwayMinutes
                        : _minHeadwayMinutes; 

                        if (sa.Entry < sb.Exit.AddMinutes(headway) && sb.Entry < sa.Exit.AddMinutes(headway))
                        {
                            conflicts.Add(new Conflict(a.Id, sa, b.Id, sb, headway));
                        }
                    }
                }
            }
        }
        return conflicts;
    }
}
