using CareerIntelligencePlatform.Domain.Entities;
using CareerIntelligencePlatform.Domain.ValueObjects;

namespace CareerIntelligencePlatform.Application.Jobs.CreateJob;

public static class CreateJobMappings
{
  public static Job ToDomain(this CreateJobCommand command)
  {
    var salary = command.Salary is null
        ? null
        : Money.Create(
            command.Salary.Amount,
            command.Salary.Currency);

    return new Job(
        command.Title,
        command.Description,
        salary);
  }

  public static SalaryResponse ToResponse(this Money money)
  {
    return new SalaryResponse(
        money.Amount,
        money.Currency);
  }

  public static CreateJobResponse ToResponse(this Job job)
  {
    return new CreateJobResponse(
        job.Id,
        job.Title,
        job.Description,
        job.Salary?.ToResponse());
  }
}