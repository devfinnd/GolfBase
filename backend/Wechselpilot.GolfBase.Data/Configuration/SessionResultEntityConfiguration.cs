using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wechselpilot.GolfBase.Data.Entities;

namespace Wechselpilot.GolfBase.Data.Configuration;

public sealed class SessionResultEntityConfiguration : IEntityTypeConfiguration<SessionResultEntity>
{
    public void Configure(EntityTypeBuilder<SessionResultEntity> builder)
    {
        builder.ToTable("SessionResults");

        builder.HasKey(x => new { x.SessionId, x.PlayerId });

        builder.Property(x => x.SessionId)
            .IsRequired();

        builder.Property(x => x.PlayerId)
            .IsRequired();

        builder.Property(x => x.Score)
            .IsRequired();

        builder.HasOne(x => x.Session)
            .WithMany(x => x.Results)
            .HasForeignKey(x => x.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}