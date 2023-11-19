using GolfBase.ApiContracts.Seasons.CreateSeason;
using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.CreateSeason;

public sealed record CreateSeasonParameters(
    [FromBody] CreateSeasonRequest Body
);