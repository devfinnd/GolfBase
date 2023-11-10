using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wechselpilot.GolfBase.Data.Entities;

namespace Wechselpilot.GolfBase.Data.Configuration;

public sealed class SeasonEntityConfiguration : IEntityTypeConfiguration<SeasonEntity>
{
    public void Configure(EntityTypeBuilder<SeasonEntity> builder)
    {
        builder.ToTable("Seasons");

        builder.HasKey(x => x.SeasonId);

        builder.Property(x => x.SeasonId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.HasMany(x => x.Maps)
            .WithMany()
            .UsingEntity<SeasonalMapAssignmentEntity>();

        builder.HasMany(x => x.Sessions)
            .WithOne(x => x.Season)
            .HasForeignKey(x => x.SeasonId);
    }
}
