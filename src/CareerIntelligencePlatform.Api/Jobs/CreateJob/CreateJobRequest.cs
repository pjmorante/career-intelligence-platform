namespace CareerIntelligencePlatform.Api.Jobs.CreateJob;

public sealed record CreateJobRequest(
    string Title,
    string Description,
    SalaryRequest? Salary);