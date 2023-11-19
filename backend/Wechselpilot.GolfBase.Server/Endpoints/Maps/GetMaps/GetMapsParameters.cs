using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMaps;

public sealed record GetMapsParameters(
    [FromQuery] int Page,
    [FromQuery] int PageSize,
    [FromQuery] string? SearchTerm
);