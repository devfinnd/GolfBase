namespace Wechselpilot.GolfBase.Server.Endpoints.Models;

public sealed record Session(
    Guid SessionId,
    DateTimeOffset Timestamp,
    SessionResult[] Results
);
