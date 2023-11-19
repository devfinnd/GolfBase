using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Seasons.GetSeasonSessions;

public sealed record GetSeasonSessionsResponse(Session[] Sessions);