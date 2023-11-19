using GolfBase.ApiContracts.Seasons.CreateSession;
using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.CreateSession;

public sealed record CreateSessionParameters(
    [FromRoute] Guid SeasonId,
    [FromBody] CreateSessionRequest Body
);