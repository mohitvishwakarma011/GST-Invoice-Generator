using FluentValidation;

namespace GI.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(200);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.StateCode).GreaterThan(0);
            RuleFor(x => x.BillingAddress).NotEmpty().NotNull();
            RuleFor(x => x.Gstin)
                .Matches(@"^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$")
                .WithMessage("Invalid GSTIN format.")
                .When(x => !string.IsNullOrWhiteSpace(x.Gstin)); // GSTIN is optional for clients
        }
    }
}
