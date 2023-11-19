using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeasons;

public sealed record GetSeasonsParameters(
    [FromQuery] int Page,
    [FromQuery] int PageSize
);