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
            .MaximumLength(1024).WithMessage("The bootstrap servers must be less than 1024 characters.");

            RuleFor(x => x.Topic)
                .NotEmpty().WithMessage("The topic must be informed.")
                .MaximumLength(255).WithMessage("The topic must be less than 255 characters.");

            RuleFor(x => x.GroupId)
                .NotEmpty().WithMessage("The group id must be informed.")
                .MaximumLength(255).WithMessage("The group id must be less than 255 characters.");

            RuleFor(x => x.AutoOffsetReset)
                .NotEmpty().WithMessage("The auto offset reset must be informed.")
                .MaximumLength(16).WithMessage("The auto offset reset must be less than 16 characters.");

            RuleFor(x => x.SecurityProtocol)
                .MaximumLength(16).WithMessage("The security protocol must be less than 16 characters.")
                .When(x => !string.IsNullOrEmpty(x.SecurityProtocol)); 

            RuleFor(x => x.SaslMechanism)
                .MaximumLength(16).WithMessage("The SASL mechanism must be less than 16 characters.")
                .When(x => !string.IsNullOrEmpty(x.SaslMechanism)); 

            RuleFor(x => x.SaslUsername)
                .MaximumLength(255).WithMessage("The SASL username must be less than 255 characters.")
                .When(x => !string.IsNullOrEmpty(x.SaslUsername)); 

            RuleFor(x => x.SaslPassword)
                .MaximumLength(255).WithMessage("The SASL password must be less than 255 characters.")
                .When(x => !string.IsNullOrEmpty(x.SaslPassword)); 

            RuleFor(x => x.SslCaLocation)
                .MaximumLength(1024).WithMessage("The SSL CA location must be less than 1024 characters.")
                .When(x => !string.IsNullOrEmpty(x.SslCaLocation));

            RuleFor(x => x.NodeId)
                .NotEmpty()
                .WithMessage("Node ID must be informed.");
        }
    }
}
