using GI.Application.DataTransferObjects.Dashboard;
using MediatR;

namespace GI.Application.Features.Dashboard.Queries.GetRecentClient
{
    public class GetRecentClientQuery : IRequest<IList<RecentClientDto>>
    {
        public int UserId { get; set; }
    }
}
