using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wechselpilot.GolfBase.Data.Entities;

namespace Wechselpilot.GolfBase.Data.Configuration;

public sealed class MapEntityConfiguration : IEntityTypeConfiguration<MapEntity>
{
    public void Configure(EntityTypeBuilder<MapEntity> builder)
    {
        builder.ToTable("Maps");

        builder.HasKey(x => x.MapId);

        builder.Property(x => x.MapId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Par)
            .IsRequired();

        builder.Property(x => x.Holes)
            .IsRequired();

        builder.HasMany(x => x.Sessions)
            .WithOne(x => x.Map)
            .HasForeignKey(x => x.MapId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
