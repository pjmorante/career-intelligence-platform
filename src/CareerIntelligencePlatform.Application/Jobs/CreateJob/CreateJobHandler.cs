using CareerIntelligencePlatform.Application.Abstractions;
using FluentValidation;

namespace CareerIntelligencePlatform.Application.Jobs.CreateJob;

public sealed class CreateJobHandler
{
  private readonly IJobRepository _jobRepository;
  private readonly IValidator<CreateJobCommand> _validator;

  public CreateJobHandler(
      IJobRepository jobRepository,
      IValidator<CreateJobCommand> validator)
  {
    _jobRepository = jobRepository;
    _validator = validator;
  }

  public async Task<CreateJobResponse> HandleAsync(
        CreateJobCommand command,
        CancellationToken cancellationToken)
  {
    var validationResult = await _validator.ValidateAsync(
        command,
        cancellationToken);

    if (!validationResult.IsValid)
    {
      throw new ValidationException(validationResult.Errors);
    }

    var job = command.ToDomain();

    await _jobRepository.AddAsync(
        job,
        cancellationToken);

    return job.ToResponse();
  }
}