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

  [Fact]
  public void Constructor_ShouldThrow_WhenTitleIsEmpty()
  {
    // Arrange
    const string title = "";
    const string description = "Backend development using ASP.NET Core.";

    // Act & Assert
    Assert.Throws<ArgumentException>(
        () => new Job(title, description));
  }

  [Fact]
  public void Constructor_ShouldThrow_WhenDescriptionIsEmpty()
  {
    // Arrange
    const string title = "Senior .NET Developer";
    const string description = "";

    // Act & Assert
    Assert.Throws<ArgumentException>(
        () => new Job(title, description));
  }
}