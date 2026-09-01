using CareerIntelligencePlatform.Domain.Exceptions;
using CareerIntelligencePlatform.Domain.ValueObjects;

namespace CareerIntelligencePlatform.Domain.Entities;

public class Job
{
  public Guid Id { get; private set; }

  public string Title { get; private set; }

  public string Description { get; private set; }

  public Money? Salary { get; private set; }

  private Job()
  {
  }

  public Job(
      string title,
      string description,
      Money? salary = null)
  {
    if (string.IsNullOrWhiteSpace(title))
      throw new DomainException(
          DomainErrorCodes.JobTitleRequired,
          "Job title is required.");

    if (title.Length > 200)
      throw new DomainException(
          DomainErrorCodes.JobTitleTooLong,
          "Job title cannot exceed 200 characters.");

    if (string.IsNullOrWhiteSpace(description))
      throw new DomainException(
          DomainErrorCodes.JobDescriptionRequired,
          "Job description is required.");

    if (description.Length > 5000)
      throw new DomainException(
          DomainErrorCodes.JobDescriptionTooLong,
          "Job description cannot exceed 5000 characters.");

    Id = Guid.NewGuid();
    Title = title;
    Description = description;
    Salary = salary;
  }
}