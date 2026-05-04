using GI.Application.DataTransferObjects.Client;
using MediatR;

namespace GI.Application.Features.Clients.GetClients
{
    public class GetClientsQuery :BasePaginationQuery, IRequest<IList<ClientDto>>
    {
        public int UserId { get; set; }
    }
}
