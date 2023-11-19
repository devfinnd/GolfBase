namespace GolfBase.ApiContracts.Seasons.CreateSeason;

public sealed record CreateSeasonRequest(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    Guid[] MapIds
);