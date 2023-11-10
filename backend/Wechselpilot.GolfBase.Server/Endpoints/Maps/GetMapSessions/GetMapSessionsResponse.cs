using Wechselpilot.GolfBase.Server.Endpoints.Models;

namespace Wechselpilot.GolfBase.Server.Endpoints.Maps.GetMapSessions;

public sealed record GetMapSessionsResponse(Session[] Sessions);