namespace CareerIntelligencePlatform.Application.Jobs.CreateJob;

public sealed record CreateJobCommand(
    string Title,
    string Description,
    SalaryInput? Salary);