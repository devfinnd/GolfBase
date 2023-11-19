namespace GolfBase.ApiContracts.Models;

public sealed record Season(
    Guid SeasonId,
    string Name,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    Map[] Maps
);