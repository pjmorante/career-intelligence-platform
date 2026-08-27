using CareerIntelligencePlatform.Application.Jobs.CreateJob;

namespace CareerIntelligencePlatform.Api.Jobs.CreateJob;

public static class CreateJobRequestMappings
{
  public static CreateJobCommand ToCommand(
      this CreateJobRequest request)
  {
    var salary = request.Salary is null
        ? null
        : new SalaryInput(
            request.Salary.Amount,
            request.Salary.Currency);

    return new CreateJobCommand(
        request.Title,
        request.Description,
        salary);
  }
}