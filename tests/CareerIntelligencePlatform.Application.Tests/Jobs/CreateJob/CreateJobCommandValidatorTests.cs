using CareerIntelligencePlatform.Application.Jobs.CreateJob;

namespace CareerIntelligencePlatform.Application.Tests.Jobs.CreateJob;

public sealed class CreateJobCommandValidatorTests
{
    private readonly CreateJobCommandValidator _validator = new();

    [Fact]
    public async Task Validate_ShouldFail_WhenTitleIsEmpty()
    {
        var command = new CreateJobCommand(
            "",
            "Backend development using ASP.NET Core.",
            null);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == "Title");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenTitleExceedsMaximumLength()
    {
        var command = new CreateJobCommand(
            new string('A', 201),
            "Backend development using ASP.NET Core.",
            null);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == "Title");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenDescriptionIsEmpty()
    {
        var command = new CreateJobCommand(
            "Senior .NET Developer",
            "",
            null);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == "Description");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenDescriptionExceedsMaximumLength()
    {
        var command = new CreateJobCommand(
            "Senior .NET Developer",
            new string('A', 5001),
            null);

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == "Description");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenSalaryAmountIsNegative()
    {
        var command = new CreateJobCommand(
            "Senior .NET Developer",
            "Backend development using ASP.NET Core.",
            new SalaryInput(
                -1m,
                "COP"));

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == "Salary.Amount");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenSalaryCurrencyIsEmpty()
    {
        var command = new CreateJobCommand(
            "Senior .NET Developer",
            "Backend development using ASP.NET Core.",
            new SalaryInput(
                9000000m,
                ""));

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == "Salary.Currency");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenSalaryCurrencyExceedsMaximumLength()
    {
        var command = new CreateJobCommand(
            "Senior .NET Developer",
            "Backend development using ASP.NET Core.",
            new SalaryInput(
                9000000m,
                "USDD"));

        var result = await _validator.ValidateAsync(command);

        Assert.False(result.IsValid);
        Assert.Contains(
            result.Errors,
            error => error.PropertyName == "Salary.Currency");
    }

    [Fact]
    public async Task Validate_ShouldSucceed_WhenCommandIsValid()
    {
        var command = new CreateJobCommand(
            "Senior .NET Developer",
            "Backend development using ASP.NET Core.",
            new SalaryInput(
                9000000m,
                "COP"));

        var result = await _validator.ValidateAsync(command);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}