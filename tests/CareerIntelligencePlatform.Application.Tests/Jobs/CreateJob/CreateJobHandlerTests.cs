using CareerIntelligencePlatform.Application.Abstractions;
using CareerIntelligencePlatform.Application.Jobs.CreateJob;
using CareerIntelligencePlatform.Domain.Entities;
using CareerIntelligencePlatform.Domain.Exceptions;
using Moq;

namespace CareerIntelligencePlatform.Application.Tests.Jobs.CreateJob;

public class CreateJobHandlerTests
{
  [Fact]
  public async Task HandleAsync_ShouldCreateAndPersistJob_WhenCommandIsValid()
  {
    // Arrange
    var repository = new Mock<IJobRepository>();

    var handler = new CreateJobHandler(repository.Object);

    var command = new CreateJobCommand(
        "Senior .NET Developer",
        "Backend development using ASP.NET Core.",
        new SalaryInput(
            9000000m,
            "COP"));

    // Act
    var response = await handler.HandleAsync(
        command,
        CancellationToken.None);

    // Assert
    Assert.NotEqual(Guid.Empty, response.Id);
    Assert.Equal(command.Title, response.Title);
    Assert.Equal(command.Description, response.Description);

    Assert.NotNull(response.Salary);
    Assert.Equal(9000000m, response.Salary.Amount);
    Assert.Equal("COP", response.Salary.Currency);

    repository.Verify(
        repository => repository.AddAsync(
            It.Is<Job>(job =>
                job.Title == command.Title &&
                job.Description == command.Description &&
                job.Salary != null &&
                job.Salary.Amount == 9000000m &&
                job.Salary.Currency == "COP"),
            It.IsAny<CancellationToken>()),
        Times.Once);
  }

  [Fact]
  public async Task HandleAsync_ShouldPropagateDomainException_WhenCommandIsInvalid()
  {
    // Arrange
    var repository = new Mock<IJobRepository>();
    var handler = new CreateJobHandler(repository.Object);

    var command = new CreateJobCommand(
        "",
        "Backend development using ASP.NET Core.",
        null);

    // Act
    var exception = await Assert.ThrowsAsync<DomainException>(
        () => handler.HandleAsync(
            command,
            CancellationToken.None));

    // Assert
    Assert.Equal(
        DomainErrorCodes.JobTitleRequired,
        exception.Code);

    repository.Verify(
        repository => repository.AddAsync(
            It.IsAny<Job>(),
            It.IsAny<CancellationToken>()),
        Times.Never);
  }

  [Fact]
  public async Task HandleAsync_ShouldCreateJobWithoutSalary_WhenSalaryIsNotProvided()
  {
    // Arrange
    var repository = new Mock<IJobRepository>();
    var handler = new CreateJobHandler(repository.Object);

    var command = new CreateJobCommand(
        "Backend Developer",
        "Backend development using ASP.NET Core.",
        null);

    // Act
    var response = await handler.HandleAsync(
        command,
        CancellationToken.None);

    // Assert
    Assert.NotEqual(Guid.Empty, response.Id);
    Assert.Equal(command.Title, response.Title);
    Assert.Equal(command.Description, response.Description);
    Assert.Null(response.Salary);

    repository.Verify(
        repository => repository.AddAsync(
            It.Is<Job>(job =>
                job.Title == command.Title &&
                job.Description == command.Description &&
                job.Salary == null),
            It.IsAny<CancellationToken>()),
        Times.Once);
  }
}