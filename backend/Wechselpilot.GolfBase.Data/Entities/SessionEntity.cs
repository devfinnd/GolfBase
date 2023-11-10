using System.Diagnostics.CodeAnalysis;

namespace Wechselpilot.GolfBase.Data.Entities;

public sealed record SessionEntity
{
    public Guid SessionId { get; private init; }
    public Guid SeasonId { get; init; }
    public Guid MapId { get; init; }
    public DateTimeOffset Timestamp { get; private init; }

    public MapEntity Map { get; private init; } = null!;
    public SeasonEntity Season { get; private init; } = null!;
    public ICollection<SessionResultEntity> Results { get; init; } = new List<SessionResultEntity>();

    [SetsRequiredMembers]
    public SessionEntity(Guid seasonId, Guid mapId, ICollection<SessionResultEntity> results)
    {
        MapId = mapId;
        SeasonId = seasonId;
        Timestamp = DateTimeOffset.UtcNow;
        Results = results;
    }

    public SessionEntity() { }
}
