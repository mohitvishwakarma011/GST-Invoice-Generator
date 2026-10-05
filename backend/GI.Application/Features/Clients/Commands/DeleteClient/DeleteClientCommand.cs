using MediatR;

namespace GI.Application.Features.Clients.Commands.DeleteClient
{
    public record DeleteClientCommand(int ClientId, int UserId) : IRequest;
}
