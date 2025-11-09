using System.Reflection;
using CleanTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTemplate.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    
    public DbSet<Customer> Customers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
