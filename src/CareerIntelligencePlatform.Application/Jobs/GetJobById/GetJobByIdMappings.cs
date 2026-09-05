using CareerIntelligencePlatform.Application.Jobs.CreateJob;

namespace CareerIntelligencePlatform.Application.Jobs.GetJobById;

public static class GetJobByIdMappings
{
  public static GetJobByIdResponse ToResponse(
      this Domain.Entities.Job job)
  {
    return new GetJobByIdResponse(
        job.Id,
        job.Title,
        job.Description,
        job.Salary is null
            ? null
            : new SalaryResponse(
                job.Salary.Amount,
                job.Salary.Currency));
  }
}