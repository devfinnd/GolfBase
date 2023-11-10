using Wechselpilot.GolfBase.Data.Entities;
using Wechselpilot.GolfBase.Server.Endpoints.Models;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeason;

public sealed record GetSeasonResponse(
    Season Season
);