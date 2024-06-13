using Aero.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Aero.Database;

public class AeroDbContext : DbContext
{
    public DbSet<AirlineDb> Airlines { get; set; }
    public DbSet<FlightDb> Flights { get; set; }
    public AeroDbContext(DbContextOptions<AeroDbContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       
    }
}