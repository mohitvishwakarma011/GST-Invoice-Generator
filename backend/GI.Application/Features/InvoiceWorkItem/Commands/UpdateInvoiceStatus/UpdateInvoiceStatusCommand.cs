using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Commands.UpdateInvoiceStatus
{
    public class UpdateInvoiceStatusCommand : IRequest<int>
    {
        public int InvoiceId { get; set; }
        public int UserId { get; set; }
        public InvoiceStatus NewStatus { get; set; }
    }
}
