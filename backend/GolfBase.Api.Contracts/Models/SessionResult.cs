namespace GolfBase.ApiContracts.Models;

public sealed record SessionResult(
    string PlayerName,
    int Score
);