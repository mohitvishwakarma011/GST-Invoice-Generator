using GI.Application.DataTransferObjects.Client;
using MediatR;

namespace GI.Application.Features.Clients.Queries.GetClientById
{
    public class GetClinetByIdQuery : IRequest<ClientDto>
    {
        public int UserId { get; set; }
        public int ClientId { get; set; }
    }
}
