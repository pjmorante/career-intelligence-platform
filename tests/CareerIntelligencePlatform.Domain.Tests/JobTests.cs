using CareerIntelligencePlatform.Domain.Entities;

namespace CareerIntelligencePlatform.Domain.Tests;

public class JobTests
{
  [Fact]
  public void Constructor_ShouldCreateJob_WhenDataIsValid()
  {
    // Arrange
    const string title = "Senior .NET Developer";
    const string description = "Backend development using ASP.NET Core.";

    // Act
    var job = new Job(title, description);

    // Assert
    Assert.NotEqual(Guid.Empty, job.Id);
    Assert.Equal(title, job.Title);
    Assert.Equal(description, job.Description);
  }
}