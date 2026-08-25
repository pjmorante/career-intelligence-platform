namespace CareerIntelligencePlatform.Domain.Entities;

public class Job
{
  public Guid Id { get; private set; }

  public string Title { get; private set; }

  public string Description { get; private set; }

  public Job(string title, string description)
  {
    if (string.IsNullOrWhiteSpace(title))
      throw new ArgumentException("Job title is required.", nameof(title));

    if (title.Length > 200)
      throw new ArgumentException(
          "Job title cannot exceed 200 characters.",
          nameof(title));

    if (string.IsNullOrWhiteSpace(description))
      throw new ArgumentException(
          "Job description is required.",
          nameof(description));

    if (description.Length > 5000)
      throw new ArgumentException(
          "Job description cannot exceed 5000 characters.",
          nameof(description));

    Id = Guid.NewGuid();
    Title = title;
    Description = description;
  }
}