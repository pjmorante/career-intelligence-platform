using CareerIntelligencePlatform.Application.Jobs.CreateJob;
using CareerIntelligencePlatform.Infrastructure.Persistence;
using CareerIntelligencePlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CareerIntelligencePlatform.Infrastructure.Tests.Integration;

public sealed class CreateJobHandlerIntegrationTests
{
    [Fact]
    public async Task HandleAsync_ShouldPersistJobWithSalary()
    {
        var connectionString = Environment.GetEnvironmentVariable(
            "CareerIntelligenceDatabase")
            ?? throw new InvalidOperationException(
                "Environment variable 'CareerIntelligenceDatabase' was not configured.");

        var options = new DbContextOptionsBuilder<CareerIntelligenceDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        await using var context = new CareerIntelligenceDbContext(options);

        var repository = new JobRepository(context);
        var handler = new CreateJobHandler(
        repository,
        new CreateJobCommandValidator());

        var command = new CreateJobCommand(
            "Senior .NET Developer",
            "Backend development using ASP.NET Core.",
            new SalaryInput(9000000m, "COP"));

        var response = await handler.HandleAsync(
            command,
            CancellationToken.None);

        var persistedJob = await context.Jobs
            .AsNoTracking()
            .SingleAsync(job => job.Id == response.Id);

        Assert.Equal("Senior .NET Developer", persistedJob.Title);
        Assert.Equal(
            "Backend development using ASP.NET Core.",
            persistedJob.Description);

        Assert.NotNull(persistedJob.Salary);
        Assert.Equal(9000000m, persistedJob.Salary!.Amount);
        Assert.Equal("COP", persistedJob.Salary.Currency);
    }
}