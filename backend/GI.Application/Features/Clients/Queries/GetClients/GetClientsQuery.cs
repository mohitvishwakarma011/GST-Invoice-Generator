using GI.Application.DataTransferObjects.Client;
using GI.Application.Features.Global.Queries;
using MediatR;

namespace GI.Application.Features.Clients.Queries.GetClients
{
    public class GetClientsQuery :BasePaginationQuery, IRequest<IList<ClientDto>>
    {
        public int UserId { get; set; }
    }
}
