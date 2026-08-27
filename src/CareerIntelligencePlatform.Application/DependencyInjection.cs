using CareerIntelligencePlatform.Application.Jobs.CreateJob;
using Microsoft.Extensions.DependencyInjection;

namespace CareerIntelligencePlatform.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(
      this IServiceCollection services)
  {
    services.AddScoped<CreateJobHandler>();

    return services;
  }
}