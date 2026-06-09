using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicePdf
{
    public class GetInvoicePdfQuery : IRequest<InvoiceDetailForPdfDto>
    {
        public int UserId { get; set; }
        public int InvoiceId { get; set; }
    }
}
