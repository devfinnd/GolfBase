using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayer;

public sealed record GetPlayerParameters(
    [FromRoute] Guid PlayerId
);