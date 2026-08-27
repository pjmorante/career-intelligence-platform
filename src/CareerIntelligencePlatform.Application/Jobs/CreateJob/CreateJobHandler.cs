using CareerIntelligencePlatform.Application.Abstractions;

namespace CareerIntelligencePlatform.Application.Jobs.CreateJob;

public sealed class CreateJobHandler
{
  private readonly IJobRepository _jobRepository;

  public CreateJobHandler(IJobRepository jobRepository)
  {
    _jobRepository = jobRepository;
  }

  public async Task<CreateJobResponse> HandleAsync(
      CreateJobCommand command,
      CancellationToken cancellationToken)
  {
    var job = command.ToDomain();

    await _jobRepository.AddAsync(
        job,
        cancellationToken);

    return job.ToResponse();
  }
}