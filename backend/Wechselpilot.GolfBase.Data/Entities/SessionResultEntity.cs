using System.Diagnostics.CodeAnalysis;

namespace Wechselpilot.GolfBase.Data.Entities;

public sealed record SessionResultEntity
{
    public required Guid PlayerId { get; init; }
    public required Guid SessionId { get; init; }
    public required int Score { get; init; }

    public PlayerEntity Player { get; private set; } = null!;
    public SessionEntity Session { get; private set; } = null!;

    [SetsRequiredMembers]
    public SessionResultEntity(Guid sessionId, Guid playerId, int score)
    {
        SessionId = sessionId;
        PlayerId = playerId;
        Score = score;
    }

    public SessionResultEntity() { }
}
