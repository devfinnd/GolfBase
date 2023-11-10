using System.Diagnostics.CodeAnalysis;

namespace Wechselpilot.GolfBase.Data.Entities;

public sealed record SeasonEntity
{
    public Guid SeasonId { get; private init; }
    public required string Name { get; init; }
    public required DateTimeOffset StartDate { get; init; }
    public required DateTimeOffset EndDate { get; init; }
    public required ICollection<MapEntity> Maps { get; init; }
    public ICollection<SessionEntity> Sessions { get; init; } = new List<SessionEntity>();

    [SetsRequiredMembers]
    public SeasonEntity(string name, DateTimeOffset startDate, DateTimeOffset endDate, ICollection<MapEntity> maps)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        Maps = maps;
    }

    public SeasonEntity() { }
}
