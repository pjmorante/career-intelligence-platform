namespace CareerIntelligencePlatform.Api.Jobs.CreateJob;

public sealed record SalaryRequest(
    decimal Amount,
    string Currency);