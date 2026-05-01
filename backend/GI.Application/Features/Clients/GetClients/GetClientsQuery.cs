using GI.Application.DataTransferObjects.Client;
using MediatR;

namespace GI.Application.Features.Clients.GetClients
{
    public record GetClientsQuery(int UserId) : IRequest<IList<ClientDto>>;
}
