using GolfBase.ApiContracts.Models;

namespace GolfBase.ApiContracts.Seasons.CreateSession;

public sealed record CreateSessionRequest(
    Guid MapId,
    IEnumerable<PlayerResult> Results
);