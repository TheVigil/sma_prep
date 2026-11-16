using sma_prep;
using sma_prep.classes;
using sma_prep.interfaces;
using sma_prep.rules;

var repository = new TrainRepository("./data/trains.json");
var validator = new TrainValidator(new ITrainRule[]
{
    new MissingIdRule(),
    new FreightLoadLimitRule(1500),
    new NoSectionsRule()
});
var conflictDetector = new ConflictDetector(3, 5);
var reportFormatter = new ReportFormatter();

new TimetableBasics.TimetableService(repository, validator, conflictDetector, reportFormatter).Run();