using Microsoft.EntityFrameworkCore;
using PlantControlPanel.Domain;

namespace PlantControlPanel.Infrastructure.Persistence.Context;

public class RollDbContext : DbContext
{
    public RollDbContext(DbContextOptions<RollDbContext> options) : base(options) { }
    
    public DbSet<Roll> Rolls => Set<Roll>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RollDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
}