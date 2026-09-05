using CareerIntelligencePlatform.Domain.Entities;
using CareerIntelligencePlatform.Domain.ValueObjects;
using CareerIntelligencePlatform.Infrastructure.Persistence;
using CareerIntelligencePlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CareerIntelligencePlatform.Infrastructure.Tests;

public class JobRepositoryTests
{
    [Fact]
    public async Task AddAsync_ShouldPersistJobWithSalary()
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

        var salary = Money.Create(9000000m, "COP");

        var job = new Job(
            "Senior .NET Developer",
            "Backend development using ASP.NET Core.",
            salary);

        await repository.AddAsync(job, CancellationToken.None);

        var persistedJob = await context.Jobs
            .AsNoTracking()
            .SingleAsync(x => x.Id == job.Id);

        Assert.Equal(job.Id, persistedJob.Id);
        Assert.Equal(job.Title, persistedJob.Title);
        Assert.Equal(job.Description, persistedJob.Description);
        Assert.NotNull(persistedJob.Salary);
        Assert.Equal(9000000m, persistedJob.Salary!.Amount);
        Assert.Equal("COP", persistedJob.Salary.Currency);
    }
}