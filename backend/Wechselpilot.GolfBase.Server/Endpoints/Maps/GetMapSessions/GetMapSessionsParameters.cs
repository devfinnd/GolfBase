using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMapSessions;

public sealed record GetMapSessionsParameters(
    [FromRoute] Guid MapId,
    [FromQuery] DateTime? From,
    [FromQuery] DateTime? To
);