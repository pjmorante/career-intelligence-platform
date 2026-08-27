namespace CareerIntelligencePlatform.Application.Jobs.CreateJob;

public sealed record CreateJobResponse(
    Guid Id,
    string Title,
    string Description,
    SalaryResponse? Salary);