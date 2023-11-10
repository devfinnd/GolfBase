using Wechselpilot.GolfBase.Server.Endpoints.Models;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeasons;

public sealed record GetSeasonsResponse(
    SeasonListItem[] Seasons,
    int TotalItems,
    int TotalPages
);
