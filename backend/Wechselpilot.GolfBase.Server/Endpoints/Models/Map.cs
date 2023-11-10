namespace Wechselpilot.GolfBase.Server.Endpoints.Models;

public sealed record Map(
    Guid MapId,
    string Name,
    int Par,
    int Holes
);
