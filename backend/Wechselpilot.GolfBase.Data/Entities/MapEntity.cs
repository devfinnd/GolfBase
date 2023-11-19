using System.Diagnostics.CodeAnalysis;

namespace Wechselpilot.GolfBase.Data.Entities;

public sealed record MapEntity
{
    public Guid MapId { get; private init; }
    public required string Name { get; set; }
    public required int Par { get; set; }
    public required int Holes { get; set; }

    public ICollection<SessionEntity> Sessions { get; set; } = new List<SessionEntity>();

    [SetsRequiredMembers]
    public MapEntity(string name, int par, int holes)
    {
        Name = name;
        Par = par;
        Holes = holes;
    }

    private MapEntity()
    {
    }
}