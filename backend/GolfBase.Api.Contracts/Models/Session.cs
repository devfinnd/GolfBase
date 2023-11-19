namespace GolfBase.ApiContracts.Models;

public sealed record Session(
    Guid SessionId,
    DateTimeOffset Timestamp,
    SessionResult[] Results
);