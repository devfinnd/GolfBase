namespace GolfBase.ApiContracts.Models;

public sealed record MapListItem(
    Guid MapId,
    string Name,
    int Par,
    int Holes
);