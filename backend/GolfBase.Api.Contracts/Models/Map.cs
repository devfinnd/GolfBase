namespace GolfBase.ApiContracts.Models;

public sealed record Map(
    Guid MapId,
    string Name,
    int Par,
    int Holes
);