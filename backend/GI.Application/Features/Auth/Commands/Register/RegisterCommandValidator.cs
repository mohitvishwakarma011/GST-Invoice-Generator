using FluentValidation;

namespace GI.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(x => x.BusinessName)
                .NotEmpty().WithMessage("Business name is required.")
                .MaximumLength(200).WithMessage("Business name cannot exceed 200 characters.");

            RuleFor(x => x.Gstin)
                .NotEmpty().WithMessage("GSTIN is required.")
                .Length(15).WithMessage("GSTIN must be exactly 15 characters long.")
                .Matches(@"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$")
                .WithMessage("Invalid GSTIN format. (e.g., 07AAAAA1111A1Z1)");

            RuleFor(x => x.StateId)
                .GreaterThan(0).WithMessage("Please select a valid state.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");
        }

    }
}
