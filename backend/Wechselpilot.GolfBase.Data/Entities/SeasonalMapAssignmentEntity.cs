namespace Wechselpilot.GolfBase.Data.Entities;

public sealed record SeasonalMapAssignmentEntity
{
    public required Guid SeasonId { get; init; }
    public required Guid MapId { get; init; }

    public SeasonEntity Season { get; init; } = null!;
    public MapEntity Map { get; init; } = null!;
}