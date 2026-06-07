using FluentValidation;

namespace GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice
{
    public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator() {
            RuleFor(x => x.ClientId).NotEmpty().GreaterThan(0).WithMessage("ClientId is required");
            RuleFor(x => x.DueDate).NotNull().GreaterThanOrEqualTo(DateTime.Today);
            RuleFor(x => x.Items).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new CreateInvoiceItemDtoValidator());
        }
    }

    public class CreateInvoiceItemDtoValidator : AbstractValidator<CreateInvoiceItemDto>
    {
        public CreateInvoiceItemDtoValidator()
        {
            RuleFor(x => x.Rate).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
