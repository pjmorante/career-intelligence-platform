using System.Net;
using System.Net.Http.Json;
using CareerIntelligencePlatform.Application.Jobs.GetJobById;
using CareerIntelligencePlatform.Domain.Entities;
using CareerIntelligencePlatform.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CareerIntelligencePlatform.Api.Tests.Jobs;

public sealed class GetJobByIdTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public GetJobByIdTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetById_ShouldReturnOk_WhenJobExists()
    {
        var job = new Job(
            "Senior .NET Developer",
            "Backend development using ASP.NET Core.");

        using (var scope = _factory.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<CareerIntelligenceDbContext>();

            dbContext.Jobs.Add(job);
            await dbContext.SaveChangesAsync();
        }

        var response = await _client.GetAsync(
            $"/api/jobs/{job.Id}");

        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);

        var body = await response.Content
            .ReadFromJsonAsync<GetJobByIdResponse>();

        Assert.NotNull(body);
        Assert.Equal(job.Id, body.Id);
        Assert.Equal(job.Title, body.Title);
        Assert.Equal(job.Description, body.Description);

        using var cleanupScope = _factory.Services.CreateScope();

        var cleanupDbContext = cleanupScope.ServiceProvider
            .GetRequiredService<CareerIntelligenceDbContext>();

        var persistedJob = await cleanupDbContext.Jobs.FindAsync(job.Id);

        if (persistedJob is not null)
        {
            cleanupDbContext.Jobs.Remove(persistedJob);
            await cleanupDbContext.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenJobDoesNotExist()
    {
        var id = Guid.NewGuid();

        var response = await _client.GetAsync(
            $"/api/jobs/{id}");

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}