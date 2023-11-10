using Wechselpilot.GolfBase.Server.Endpoints.Models;

namespace Wechselpilot.GolfBase.Server.Endpoints.Players.GetPlayerSessions;

public sealed record GetPlayerSessionsResponse(Session[] Sessions);