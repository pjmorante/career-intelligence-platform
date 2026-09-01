using CareerIntelligencePlatform.Application.Abstractions;
using CareerIntelligencePlatform.Infrastructure.Persistence;
using CareerIntelligencePlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareerIntelligencePlatform.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString(
        "CareerIntelligenceDatabase");

    services.AddDbContext<CareerIntelligenceDbContext>(options =>
        options.UseSqlServer(connectionString));

    services.AddScoped<IJobRepository, JobRepository>();

    return services;
  }
}