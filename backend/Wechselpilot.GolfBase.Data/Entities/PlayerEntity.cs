using System.Diagnostics.CodeAnalysis;

namespace Wechselpilot.GolfBase.Data.Entities;

public sealed record PlayerEntity
{
    public Guid PlayerId { get; private init; }
    public required string Name { get; init; }

    public ICollection<SessionEntity> Sessions { get; init; } = new List<SessionEntity>();

    [SetsRequiredMembers]
    public PlayerEntity(string name)
    {
        Name = name;
    }

    private PlayerEntity()
    {
    }
}