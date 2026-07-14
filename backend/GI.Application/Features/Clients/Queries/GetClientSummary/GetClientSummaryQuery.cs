using GI.Application.DataTransferObjects.Client;
using MediatR;

namespace GI.Application.Features.Clients.Queries.GetClientSummary
{
    public class GetClientSummaryQuery : IRequest<ClientSummaryDto>
    {
        public int UserId { get; set; }
        public int ClientId { get; set; }
    }
}
