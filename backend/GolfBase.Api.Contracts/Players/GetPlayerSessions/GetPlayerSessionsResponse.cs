using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Players.GetPlayerSessions;

public sealed record GetPlayerSessionsResponse(Session[] Sessions);