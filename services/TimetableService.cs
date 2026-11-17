using sma_prep.rules;
using sma_prep.trains;
using sma_prep.trainRepositories;
using sma_prep.conflicts;
using sma_prep.reporting;
using sma_prep.policies;

namespace TimetableBasics;

public class TimetableService(ITrainRepository repository, TrainValidator trainValidator, ConflictDetector conflictDetector, ReportFormatter reportFormatter /*IEnumerable<IReportSink> reportSinks, INotifier notifier */)
{

    private readonly ITrainRepository _repository = repository; 
    private TrainValidator _trainValidator = trainValidator;
    private ConflictDetector _conflictDetector = conflictDetector;
    private ReportFormatter _reportFormatter = reportFormatter;
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
        var conflicts = _conflictDetector.DetectConflicts(trains);

        // 5. Print report
        _reportFormatter.PrintReport(conflicts);

        // 6. Save report
        // File.WriteAllText("./conflicts.txt", sb2.ToString());

        // 7. Notify planners
        if (conflicts.Count > 0)
            Console.WriteLine("Sending email to planning@example.com: " + conflicts.Count + " conflicts found.");
    }
}
