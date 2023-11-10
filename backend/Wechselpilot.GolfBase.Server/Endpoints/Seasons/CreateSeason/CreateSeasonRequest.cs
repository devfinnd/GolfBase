namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.CreateSeason;

public sealed record CreateSeasonRequest(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    Guid[] MapIds
);
