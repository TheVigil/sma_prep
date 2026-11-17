using System;
using System.Text;
using sma_prep.trains;

namespace sma_prep.reporting;

public class ReportFormatter
{

    public void PrintReport(IReadOnlyList<Conflict> conflicts)
    {
        var sb2 = new StringBuilder();
        sb2.AppendLine("=== Conflict report " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + " ===");
        if (conflicts.Count == 0)
        {
            sb2.AppendLine("No conflicts found.");
        }
        else
        {
            foreach (var c in conflicts)
                sb2.AppendLine(" - " + c);
        }
        Console.WriteLine(sb2.ToString());
    }
}
