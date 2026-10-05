using GI.Application.DataTransferObjects.Dashboard;
using MediatR;

namespace GI.Application.Features.Dashboard.Queries.GetRecentInvoiceItem
{
    public class GetRecentInvoiceItemQuery : IRequest<IList<RecentInvoiceItem>>
    {
        public int UserId {  get; set; }
    }
}
