using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using sma_prep;
using sma_prep.classes;

namespace TimetableBasics;

public class TimetableService(ITrainRepository repository, TrainValidator trainValidator, ConflictDetector conflictDetector, ReportFormatter reportFormatter, IEnumerable<IReportSink> reportSinks, INotifier notifier)
{

    private readonly ITrainRepository _repository = repository; 
    private TrainValidator _trainValidator = trainValidator;
    
    private ConflictDetector _conflictDetector = conflictDetector;

    private const int MinHeadwayMinutes = 3;
    private const int FreightHeadwayMinutes = 5;

    private SpeedRestriction restriction = new();
    public void Run()
    {
        // 1. Load trains
        var trains = _repository.GetTrains();

        // 2. Validate
        var validationMessages = _trainValidator.ValidateTrains((List<Train>)trains);

        // 3. Apply a temporary speed restriction to all trains
        restriction.ApplyRestriction(trains, 120.0);

        // 4. Detect conflicts
        _conflictDetector.DetectConflicts(trains);

        // 5. Print report
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

        // 6. Save report
        File.WriteAllText("./conflicts.txt", sb2.ToString());

        // 7. Notify planners
        if (conflicts.Count > 0)
            Console.WriteLine("Sending email to planning@example.com: " + conflicts.Count + " conflicts found.");
    }
}
