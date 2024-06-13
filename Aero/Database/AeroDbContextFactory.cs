using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Aero.Database;

public class AeroDbContextFactory : IDesignTimeDbContextFactory<AeroDbContext>
{
    public AeroDbContext CreateDbContext(string[] args)
    {
        var fileSettingsName = "appsettings.json";
        if(Path.Exists(Directory.GetCurrentDirectory() + "\\appsettings.Development.json"))
            fileSettingsName = "appsettings.Development.json";
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(fileSettingsName, optional: false);

        IConfiguration config = builder.Build();

        var connectionString = config.GetConnectionString("Default");
        var optionsBuilder = new DbContextOptionsBuilder<AeroDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AeroDbContext(optionsBuilder.Options);
    }
}