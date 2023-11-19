using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Maps.GetMapSessions;

public sealed record GetMapSessionsResponse(Session[] Sessions);