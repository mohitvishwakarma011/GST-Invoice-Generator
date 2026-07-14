using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;

namespace GI.Application.Features.Clients.Queries.GetClientInvoices
{
    public class GetClientInvoicesQuery : IRequest<IList<InvoiceListDto>>
    {
        public int ClientId { get; set; }
        public int UserId { get; set; }
    }
}
