using GI.Application.DataTransferObjects.Dashboard;
using MediatR;

namespace GI.Application.Features.Dashboard.Queries.GetDashboardSummary
{
    public class GetDashboardSummaryQuery : IRequest<DashboardSummaryDto>
    {
        public int UserId { get; set; }
    }
}
