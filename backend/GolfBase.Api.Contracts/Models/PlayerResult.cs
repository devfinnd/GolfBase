namespace GolfBase.ApiContracts.Models;

public sealed record PlayerResult(
    Guid PlayerId,
    int Score
);