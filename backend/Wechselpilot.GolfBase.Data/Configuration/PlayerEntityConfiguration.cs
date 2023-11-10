using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wechselpilot.GolfBase.Data.Entities;

namespace Wechselpilot.GolfBase.Data.Configuration;

public sealed class PlayerEntityConfiguration : IEntityTypeConfiguration<PlayerEntity>
{
    public void Configure(EntityTypeBuilder<PlayerEntity> builder)
    {
        builder.ToTable("Players");

        builder.HasKey(x => x.PlayerId);

        builder.Property(x => x.PlayerId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.HasMany(x => x.Sessions)
            .WithMany()
            .UsingEntity<SessionResultEntity>(
                right => right.HasOne(x => x.Session).WithMany(x => x.Results),
                left => left.HasOne(x => x.Player).WithMany()
            );
    }
}