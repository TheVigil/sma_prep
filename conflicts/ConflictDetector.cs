using System.Data;
using sma_prep.policies;
using sma_prep.trains;

namespace sma_prep.trains;

public record Conflict ( 
    string TrainIdA, SectionOccupation OccupationA, 
    string TrainIdB, SectionOccupation OccupationB,
    int HeadwayMinutes
);

public class ConflictDetector(IHeadwayPolicy headwayPolicy)
{
    private readonly IHeadwayPolicy _headwayPolicy = headwayPolicy;
    public IReadOnlyList<Conflict> DetectConflicts(IReadOnlyList<Train> trains)
    {
        var conflicts = new List<Conflict>();
        for (int i = 0; i < trains.Count; i++)
        {
            for (int j = i + 1; j < trains.Count; j++)
            {
                var a = trains[i];
                var b = trains[j];
                
                int headway = _headwayPolicy.GetHeadwayMinutes(a, b);
                
                foreach (var sa in a.Sections)
                {
                    foreach (var sb in b.Sections)
                    {
                        if (sa.SectionId != sb.SectionId)
                            continue;

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
