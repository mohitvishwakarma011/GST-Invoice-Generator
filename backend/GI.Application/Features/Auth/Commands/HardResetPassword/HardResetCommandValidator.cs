using FluentValidation;

namespace GI.Application.Features.Auth.Commands.HardResetPassword
{
    public class HardResetCommandValidator : AbstractValidator<HardResetPasswordCommand>
    {
        public HardResetCommandValidator() 
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
