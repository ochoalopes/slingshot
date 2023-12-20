using FluentValidation;
using OchoaLopes.SlingShot.Domain.Entities;

namespace OchoaLopes.SlingShot.Domain.Validators
{
    public class ApiConfigurationEntityValidator : AbstractValidator<ApiConfigurationEntity>
    {
        public ApiConfigurationEntityValidator()
        {
            RuleFor(x => x.BaseUrl)
                .NotEmpty()
                .WithMessage("Base URL be informed.");

            RuleFor(x => x.NodeId)
                .NotEmpty()
                .WithMessage("Node ID must be informed.");
        }
    }
}
