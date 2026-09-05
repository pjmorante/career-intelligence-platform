using CareerIntelligencePlatform.Domain.Entities;

namespace CareerIntelligencePlatform.Application.Abstractions;

public interface IJobRepository
{
  Task AddAsync(Job job, CancellationToken cancellationToken);
  Task<Job?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken);
}