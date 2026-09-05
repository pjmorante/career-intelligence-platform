using CareerIntelligencePlatform.Application.Jobs.CreateJob;

namespace CareerIntelligencePlatform.Application.Jobs.GetJobById;

public sealed record GetJobByIdResponse(
    Guid Id,
    string Title,
    string Description,
    SalaryResponse? Salary);