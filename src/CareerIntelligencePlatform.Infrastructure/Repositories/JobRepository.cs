using CareerIntelligencePlatform.Application.Abstractions;
using CareerIntelligencePlatform.Domain.Entities;

namespace CareerIntelligencePlatform.Infrastructure.Repositories;

public sealed class JobRepository : IJobRepository
{
  public Task AddAsync(
      Job job,
      CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}