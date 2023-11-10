using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wechselpilot.GolfBase.Data.Entities;

namespace Wechselpilot.GolfBase.Data.Configuration;

public sealed class SeasonalMapAssignmentEntityConfiguration : IEntityTypeConfiguration<SeasonalMapAssignmentEntity>
{
    public void Configure(EntityTypeBuilder<SeasonalMapAssignmentEntity> builder)
    {
        builder.ToTable("SeasonalMapAssignments");

        builder.HasKey(x => new { x.SeasonId, x.MapId });

        builder.Property(x => x.SeasonId)
            .IsRequired();

        builder.Property(x => x.MapId)
            .IsRequired();

        builder.HasOne(x => x.Season)
            .WithMany()
            .HasForeignKey(x => x.SeasonId);

        builder.HasOne(x => x.Map)
            .WithMany()
            .HasForeignKey(x => x.MapId);
    }
}