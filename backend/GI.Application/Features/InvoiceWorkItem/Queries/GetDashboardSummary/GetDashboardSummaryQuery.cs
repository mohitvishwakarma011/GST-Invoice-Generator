using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetDashboardSummary
{
    public class GetDashboardSummaryQuery : IRequest<IList<DashboardItemDto>>
    {
        public int UserId { get; set; }
    }
}
