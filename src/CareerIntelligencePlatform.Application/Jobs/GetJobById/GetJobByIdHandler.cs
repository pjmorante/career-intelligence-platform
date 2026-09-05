using CareerIntelligencePlatform.Application.Abstractions;

namespace CareerIntelligencePlatform.Application.Jobs.GetJobById;

public sealed class GetJobByIdHandler
{
  private readonly IJobRepository _jobRepository;

  public GetJobByIdHandler(IJobRepository jobRepository)
  {
    _jobRepository = jobRepository;
  }

  public async Task<GetJobByIdResponse?> HandleAsync(
      GetJobByIdQuery query,
      CancellationToken cancellationToken)
  {
    var job = await _jobRepository.GetByIdAsync(
        query.Id,
        cancellationToken);

    return job?.ToResponse();
  }
}