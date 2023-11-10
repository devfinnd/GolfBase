using Microsoft.EntityFrameworkCore;
using Wechselpilot.GolfBase.Data.Entities;

namespace Wechselpilot.GolfBase.Data;

public sealed class GolfDbContext : DbContext
{
    public DbSet<SeasonEntity> Seasons => Set<SeasonEntity>();
    public DbSet<PlayerEntity> Players => Set<PlayerEntity>();
    public DbSet<MapEntity> Maps => Set<MapEntity>();
    public DbSet<SessionEntity> Sessions => Set<SessionEntity>();
    public DbSet<SessionResultEntity> SessionResults => Set<SessionResultEntity>();

    public GolfDbContext(DbContextOptions<GolfDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GolfDbContext).Assembly);
    }
}
