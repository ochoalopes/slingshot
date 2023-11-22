using FluentValidation;
using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Domain.Validators
{
    public class StorageConfigurationEntityValidator : AbstractValidator<StorageConfigurationEntity>
    {
        public StorageConfigurationEntityValidator()
        {
            RuleFor(x => x.ConnectionString)
                .NotEmpty()
                .WithMessage("The connection string must be informed.");

            RuleFor(x => x.ContainerName)
                .NotEmpty()
                .WithMessage("The container name must be informed.");
        }
    }
}
