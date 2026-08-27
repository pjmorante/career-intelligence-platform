namespace CareerIntelligencePlatform.Application.Jobs.CreateJob;

public sealed record SalaryInput(
    decimal Amount,
    string Currency);