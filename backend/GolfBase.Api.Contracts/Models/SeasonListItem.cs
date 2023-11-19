namespace GolfBase.ApiContracts.Models;

public sealed record SeasonListItem(
    Guid SeasonId,
    string Name,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate
);