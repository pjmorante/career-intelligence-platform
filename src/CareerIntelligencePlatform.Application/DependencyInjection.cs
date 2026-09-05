using CareerIntelligencePlatform.Application.Jobs.CreateJob;
using CareerIntelligencePlatform.Application.Jobs.GetJobById;
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
    services.AddScoped<GetJobByIdHandler>();

    return services;
  }
}