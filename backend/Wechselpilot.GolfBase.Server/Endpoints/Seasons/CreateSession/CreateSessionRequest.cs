using Wechselpilot.GolfBase.Server.Endpoints.Models;

namespace Wechselpilot.GolfBase.Server.Endpoints.Seasons.CreateSession;

public sealed record CreateSessionRequest(
    Guid MapId,
    IEnumerable<PlayerResult> Results
);