using sma_prep.rules;
using sma_prep.trains;
using sma_prep.trainRepositories;
using sma_prep.reporting;
using sma_prep.policies;
using sma_prep.services;

namespace TimetableBasics;

public class TimetableService(ITrainRepository repository, TrainValidator trainValidator, ConflictDetector conflictDetector)
{

    private readonly ITrainRepository _repository = repository; 
    private TrainValidator _trainValidator = trainValidator;
    private ConflictDetector _conflictDetector = conflictDetector;
    private SpeedRestriction restriction = new();
    public async Task<TimeTableResult> RunAsync()
    {
        // 1. Load trains
        var trains = await _repository.GetAllTrainsAsync();

        // 2. Validate
        var validationMessages = _trainValidator.ValidateTrains(trains);

        // 3. Apply a temporary speed restriction to all trains
        restriction.ApplyRestriction(trains, 120.0);

        // 4. Detect conflicts
        var conflicts = _conflictDetector.DetectConflicts(trains);

        // 5. Print report
        return new TimeTableResult(validationMessages, conflicts);
    }
}
