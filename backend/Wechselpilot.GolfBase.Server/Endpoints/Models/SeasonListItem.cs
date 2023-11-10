namespace Wechselpilot.GolfBase.Server.Endpoints.Models;

public sealed record SeasonListItem(
    Guid SeasonId,
    string Name,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate
);
