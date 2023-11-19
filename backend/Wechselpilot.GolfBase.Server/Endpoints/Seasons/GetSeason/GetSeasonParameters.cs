using Microsoft.AspNetCore.Mvc;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeason;

public sealed record GetSeasonParameters(
    [FromRoute] Guid SeasonId
);