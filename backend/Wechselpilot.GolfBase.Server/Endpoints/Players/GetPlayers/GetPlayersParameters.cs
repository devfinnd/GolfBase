using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayers;

public sealed record GetPlayersParameters(
    [FromQuery] int Page,
    [FromQuery] int PageSize,
    [FromQuery] string? SearchTerm
);