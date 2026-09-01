using CareerIntelligencePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerIntelligencePlatform.Infrastructure.Persistence.Configurations;

public sealed class JobConfiguration : IEntityTypeConfiguration<Job>
{
  public void Configure(EntityTypeBuilder<Job> builder)
  {
    builder.ToTable("Jobs");

    builder.HasKey(job => job.Id);

    builder.Property(job => job.Title)
        .IsRequired()
        .HasMaxLength(200);

    builder.Property(job => job.Description)
    .IsRequired()
    .HasColumnType("nvarchar(max)");

    builder.ComplexProperty(
        job => job.Salary,
        salary =>
        {
          salary.Property(money => money.Amount)
              .HasColumnName("SalaryAmount")
              .HasPrecision(18, 2);

          salary.Property(money => money.Currency)
                  .HasColumnName("SalaryCurrency")
                  .HasMaxLength(3);
        });
  }
}