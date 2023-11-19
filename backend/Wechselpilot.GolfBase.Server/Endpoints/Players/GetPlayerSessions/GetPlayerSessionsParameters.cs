using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayerSessions;

public sealed record GetPlayerSessionsParameters(
    [FromRoute] Guid PlayerId,
    [FromQuery] DateTime? From,
    [FromQuery] DateTime? To
);