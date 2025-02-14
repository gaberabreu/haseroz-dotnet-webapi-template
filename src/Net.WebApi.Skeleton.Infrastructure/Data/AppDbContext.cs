using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Net.WebApi.Skeleton.Core.WorkItemAggregate;

namespace Net.WebApi.Skeleton.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<WorkItem> WorkItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
