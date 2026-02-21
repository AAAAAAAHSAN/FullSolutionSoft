using FullSolutionSoft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FullSolutionSoft.Infrastructure;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Make sure the connection string matches your appsettings.json
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=FullSolutionSoftDb;Trusted_Connection=True;TrustServerCertificate=True;"
        );

        return new AppDbContext(optionsBuilder.Options);
    }
}
