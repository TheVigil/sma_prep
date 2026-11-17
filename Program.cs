using sma_prep.trainRepositories;
using sma_prep.rules;
using sma_prep.policies;
using sma_prep.trains;
using sma_prep.reporting;

var repository = new TrainRepository("./data/trains.json");
var validator = new TrainValidator(new ITrainRule[]
{
    new MissingIdRule(),
    new FreightLoadLimitRule(1500),
    new NoSectionsRule()
});
var headwayPolicy = new StandardHeadwayPolicy(freightHeadwayMinutes: 5, minHeadwayMinutes: 3);
var conflictDetector = new ConflictDetector(headwayPolicy);
var reportFormatter = new ReportFormatter();

var timetableService = new TimetableBasics.TimetableService(repository, validator, conflictDetector, reportFormatter);
await timetableService.RunAsync();