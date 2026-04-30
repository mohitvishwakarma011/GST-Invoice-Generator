using FluentValidation;

namespace GI.Application.Features.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
        }
    }
}
