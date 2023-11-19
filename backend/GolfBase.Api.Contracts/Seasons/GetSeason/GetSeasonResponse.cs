using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Seasons.GetSeason;

public sealed record GetSeasonResponse(
    Season Season
);