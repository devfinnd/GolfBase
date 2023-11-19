using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMap;

public sealed record GetMapParameters(
    [FromRoute] Guid MapId
);