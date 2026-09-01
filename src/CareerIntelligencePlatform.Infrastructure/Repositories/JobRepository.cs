using CareerIntelligencePlatform.Application.Abstractions;
using CareerIntelligencePlatform.Domain.Entities;
using CareerIntelligencePlatform.Infrastructure.Persistence;

namespace CareerIntelligencePlatform.Infrastructure.Repositories;

public sealed class JobRepository : IJobRepository
{
  private readonly CareerIntelligenceDbContext _context;

  public JobRepository(CareerIntelligenceDbContext context)
  {
    _context = context;
  }

  public async Task AddAsync(
      Job job,
      CancellationToken cancellationToken)
  {
    await _context.Jobs.AddAsync(
        job,
        cancellationToken);

    await _context.SaveChangesAsync(
        cancellationToken);
  }
}