using Wechselpilot.GolfBase.Server.Endpoints.Models;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.GetSeasonSessions;

public sealed record GetSeasonSessionsResponse(Session[] Sessions);