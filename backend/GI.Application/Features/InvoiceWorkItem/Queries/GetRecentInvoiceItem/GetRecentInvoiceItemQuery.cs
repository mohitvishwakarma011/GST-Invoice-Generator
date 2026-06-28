using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetRecentInvoiceItem
{
    public class GetRecentInvoiceItemQuery : IRequest<IList<RecentInvoiceItem>>
    {
        public int UserId {  get; set; }
    }
}
