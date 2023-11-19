using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeasonSessions;

public sealed record GetSeasonSessionsParameters(
    [FromRoute] Guid SeasonId,
    [FromQuery] DateTime? From,
    [FromQuery] DateTime? To
);