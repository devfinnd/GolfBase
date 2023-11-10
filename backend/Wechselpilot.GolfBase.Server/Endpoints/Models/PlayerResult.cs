namespace Wechselpilot.GolfBase.Server.Endpoints.Models;

public sealed record PlayerResult(
    Guid PlayerId,
    int Score
);
