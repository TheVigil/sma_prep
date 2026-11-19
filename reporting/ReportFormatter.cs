using System.Text;
using sma_prep.rules;
using sma_prep.trains;   // wherever ValidationMessage lives in your project

namespace sma_prep.reporting;

public class ReportFormatter
{
    public string Format(
        IReadOnlyList<Conflict> conflicts,
        IReadOnlyList<ValidationMessage> messages,
        DateTime createdAt)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"=== Conflict report {createdAt:yyyy-MM-dd HH:mm} ===");

        sb.AppendLine();
        sb.AppendLine("Validation:");
        if (messages.Count == 0)
            sb.AppendLine("  No issues found.");
        foreach (var m in messages)
            sb.AppendLine($"  [{m.Severity}] {m.Message}");

        sb.AppendLine();
        sb.AppendLine("Conflicts:");
        if (conflicts.Count == 0)
            sb.AppendLine("  No conflicts found.");
        foreach (var c in conflicts)
            sb.AppendLine(FormatConflict(c));

        return sb.ToString();
    }

    private static string FormatConflict(Conflict c) =>
        $"  {c.TrainIdA} and {c.TrainIdB} on {c.OccupationA.SectionId}: " +
        $"{c.OccupationA.Entry:HH:mm}-{c.OccupationA.Exit:HH:mm} vs " +
        $"{c.OccupationB.Entry:HH:mm}-{c.OccupationB.Exit:HH:mm} " +
        $"(min. headway {c.HeadwayMinutes} min)";
}
