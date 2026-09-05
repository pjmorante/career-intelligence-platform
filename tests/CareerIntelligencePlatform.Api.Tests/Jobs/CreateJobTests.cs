using System.Net;
using System.Net.Http.Json;
using CareerIntelligencePlatform.Application.Jobs.CreateJob;
using Microsoft.AspNetCore.Mvc;
using CareerIntelligencePlatform.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CareerIntelligencePlatform.Api.Tests.Jobs;

public sealed class CreateJobTests
    : IClassFixture<CustomWebApplicationFactory>
{
  private readonly HttpClient _client;
  private readonly CustomWebApplicationFactory _factory;

  public CreateJobTests(CustomWebApplicationFactory factory)
  {
    _factory = factory;
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task Post_ShouldReturnBadRequest_WhenTitleIsEmpty()
  {
    var request = new
    {
      title = "",
      description = "Backend development using ASP.NET Core.",
      salary = (object?)null
    };

    var response = await _client.PostAsJsonAsync(
        "/api/jobs",
        request);

    Assert.Equal(
        HttpStatusCode.BadRequest,
        response.StatusCode);

    var body = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

    Assert.NotNull(body);
    Assert.Equal("Validation failed.", body.Title);
    Assert.Equal(400, body.Status);
    Assert.Contains("Title", body.Errors);
  }

  [Fact]
  public async Task Post_ShouldReturnCreated_WhenRequestIsValid()
  {
    var request = new
    {
      title = "Senior .NET Developer",
      description = "Backend development using ASP.NET Core.",
      salary = new
      {
        amount = 9000000m,
        currency = "COP"
      }
    };

    var response = await _client.PostAsJsonAsync(
        "/api/jobs",
        request);

    Assert.Equal(
        HttpStatusCode.Created,
        response.StatusCode);

    var body = await response.Content
        .ReadFromJsonAsync<CreateJobResponse>();

    Assert.NotNull(body);
    Assert.NotEqual(Guid.Empty, body.Id);

    using var scope = _factory.Services.CreateScope();

    var dbContext = scope.ServiceProvider
        .GetRequiredService<CareerIntelligenceDbContext>();

    var job = await dbContext.Jobs.FindAsync(body.Id);

    Assert.NotNull(job);

    dbContext.Jobs.Remove(job);

    await dbContext.SaveChangesAsync();
  }

  [Fact]
  public async Task Post_ShouldNotPersistJob_WhenTitleIsEmpty()
  {
    var request = new
    {
      title = "",
      description = "Backend development using ASP.NET Core.",
      salary = (object?)null
    };

    var response = await _client.PostAsJsonAsync(
        "/api/jobs",
        request);

    Assert.Equal(
        HttpStatusCode.BadRequest,
        response.StatusCode);

    using var scope = _factory.Services.CreateScope();

    var dbContext = scope.ServiceProvider
        .GetRequiredService<CareerIntelligenceDbContext>();

    var exists = await dbContext.Jobs
    .AnyAsync(job => job.Title == "");

    Assert.False(exists);
  }
}