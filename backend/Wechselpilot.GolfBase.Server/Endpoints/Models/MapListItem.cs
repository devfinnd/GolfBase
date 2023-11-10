namespace Wechselpilot.GolfBase.Server.Endpoints.Models;

public sealed record MapListItem(
    Guid MapId,
    string Name,
    int Par,
    int Holes
);
