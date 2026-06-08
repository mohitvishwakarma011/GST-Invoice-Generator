using FluentValidation;

namespace GI.Application.Features.InvoiceWorkItem.Commands.UpdateInvoiceStatus
{
    public class UpdateInvoiceStatusCommandValidator : AbstractValidator<UpdateInvoiceStatusCommand>
    {
        public UpdateInvoiceStatusCommandValidator() 
        {
            RuleFor(x => x.InvoiceId).GreaterThan(0).WithMessage("Invalid invoice id.");
            RuleFor(x => x.NewStatus).NotEqual(InvoiceStatus.Draft).WithMessage("Cannot revert invoice back to Draft.");
        }
    }
}
