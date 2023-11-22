using FluentValidation;
using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Domain.Validators
{
    public class KafkaConfigurationEntityValidator : AbstractValidator<KafkaConfigurationEntity>
    {
        public KafkaConfigurationEntityValidator()
        {
            RuleFor(x => x.BootstrapServers)
                .NotEmpty().WithMessage("The bootstrap servers must be informed.")
                .MaximumLength(255).WithMessage("The bootstrap servers must be less than 255 characters.");

            RuleFor(x => x.Topic)
                .NotEmpty().WithMessage("The topic must be informed.")
                .MaximumLength(255).WithMessage("The topic must be less than 255 characters.");

            RuleFor(x => x.GroupId)
                .NotEmpty()
                .WithMessage("The group id must be informed.");

            RuleFor(x => x.AutoOffsetReset)
                .NotEmpty()
                .WithMessage("The auto offset reset must be informed.");

            RuleFor(x => x.EnableAutoCommit)
                .NotEmpty()
                .WithMessage("The enable auto commit must be informed.");
        }
    }
}
