using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQuery : IRequest<InvoiceDetailDto>
    {
        public int InvoiceId { get; set; }
        public int UserId { get; set; }
    }
}
