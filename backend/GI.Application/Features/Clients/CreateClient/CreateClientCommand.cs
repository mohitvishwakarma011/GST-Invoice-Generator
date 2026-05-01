using GI.Application.DataTransferObjects.Client;
using MediatR;

namespace GI.Application.Features.Clients.CreateClient
{
    public record CreateClientCommand(
     int UserId,
     string Name,
     string? Gstin,
     string Email,
     string Address,
     string State
    ) : IRequest<ClientDto>;
}
