using FluentValidation;

namespace CareerIntelligencePlatform.Application.Jobs.CreateJob;

public sealed class CreateJobCommandValidator
    : AbstractValidator<CreateJobCommand>
{
    public CreateJobCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(command => command.Description)
            .NotEmpty()
            .MaximumLength(5000);

        When(command => command.Salary is not null, () =>
        {
            RuleFor(command => command.Salary!.Amount)
                .GreaterThanOrEqualTo(0);

            RuleFor(command => command.Salary!.Currency)
                .NotEmpty()
                .MaximumLength(3);
        });
    }
}