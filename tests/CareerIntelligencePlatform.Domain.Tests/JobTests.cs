using CareerIntelligencePlatform.Domain.Entities;
using CareerIntelligencePlatform.Domain.Exceptions;

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

  [Theory]
  [InlineData("")]
  [InlineData("   ")]
  public void Constructor_ShouldThrow_WhenTitleIsInvalid(string title)
  {
    // Arrange
    const string description = "Backend development using ASP.NET Core.";

    // Act
    var exception = Assert.Throws<DomainException>(
      () => new Job(title, description));

    Assert.Equal("JOB_TITLE_REQUIRED", exception.Code);
    Assert.Equal("Job title is required.", exception.Message);
  }

  [Fact]
  public void Constructor_ShouldThrow_WhenDescriptionIsEmpty()
  {
    // Arrange
    const string title = "Senior .NET Developer";
    const string description = "";

    // Act
    var exception = Assert.Throws<DomainException>(
    () => new Job(title, description));

    Assert.Equal("Job description is required.", exception.Message);
  }

  [Fact]
  public void Constructor_ShouldThrow_WhenTitleExceedsMaximumLength()
  {
    // Arrange
    var title = new string('A', 201);
    const string description = "Backend development using ASP.NET Core.";

    // Act
    var exception = Assert.Throws<DomainException>(
    () => new Job(title, description));

    Assert.Equal(
    "JOB_TITLE_TOO_LONG",
    exception.Code);

    Assert.Equal(
        "Job title cannot exceed 200 characters.",
        exception.Message);
  }

  [Theory]
  [InlineData(199)]
  [InlineData(200)]
  public void Constructor_ShouldCreateJob_WhenTitleLengthIsValid(int titleLength)
  {
    // Arrange
    var title = new string('A', titleLength);
    const string description = "Backend development using ASP.NET Core.";

    // Act
    var job = new Job(title, description);

    // Assert
    Assert.Equal(titleLength, job.Title.Length);
  }

  [Fact]
  public void Constructor_ShouldThrow_WhenDescriptionExceedsMaximumLength()
  {
    // Arrange
    const string title = "Senior .NET Developer";
    var description = new string('A', 5001);

    // Act
    var exception = Assert.Throws<DomainException>(
        () => new Job(title, description));

    // Assert
    Assert.Equal(
        "Job description cannot exceed 5000 characters.",
        exception.Message);
  }

  [Fact]
  public void Constructor_ShouldCreateJob_WhenDescriptionHasMaximumAllowedLength()
  {
    // Arrange
    const string title = "Senior .NET Developer";
    var description = new string('A', 5000);

    // Act
    var job = new Job(title, description);

    // Assert
    Assert.Equal(5000, job.Description.Length);
  }
}