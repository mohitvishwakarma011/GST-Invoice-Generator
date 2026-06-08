using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int InvoiceId { get; set; }
    }
}