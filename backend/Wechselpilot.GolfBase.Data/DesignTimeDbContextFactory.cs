using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Wechselpilot.GolfBase.Data;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GolfDbContext>
{
    public GolfDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<GolfDbContext> optionsBuilder = new DbContextOptionsBuilder<GolfDbContext>();
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=GolfBase;User Id=sa;Password=yourStrong!Password;TrustServerCertificate=True");

        return new GolfDbContext(optionsBuilder.Options);
    }
}