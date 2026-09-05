using CareerIntelligencePlatform.Application.Jobs.CreateJob;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CareerIntelligencePlatform.Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplication(
      this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining<CreateJobCommandValidator>();

    services.AddScoped<CreateJobHandler>();

    return services;
  }
}