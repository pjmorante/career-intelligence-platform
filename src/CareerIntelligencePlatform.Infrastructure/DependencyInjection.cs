using CareerIntelligencePlatform.Application.Abstractions;
using CareerIntelligencePlatform.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CareerIntelligencePlatform.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
      this IServiceCollection services)
  {
    services.AddScoped<IJobRepository, JobRepository>();

    return services;
  }
}