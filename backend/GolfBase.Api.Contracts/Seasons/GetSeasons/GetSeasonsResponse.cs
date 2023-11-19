using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Seasons.GetSeasons;

public sealed record GetSeasonsResponse(
    SeasonListItem[] Seasons,
    int TotalItems,
    int TotalPages
);