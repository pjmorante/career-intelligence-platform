using CareerIntelligencePlatform.Domain.Entities;
using CareerIntelligencePlatform.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CareerIntelligencePlatform.Infrastructure.Persistence;

public sealed class CareerIntelligenceDbContext : DbContext
{
  public CareerIntelligenceDbContext(
      DbContextOptions<CareerIntelligenceDbContext> options)
      : base(options)
  {
  }
  public DbSet<Job> Jobs => Set<Job>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new JobConfiguration());
  }
}