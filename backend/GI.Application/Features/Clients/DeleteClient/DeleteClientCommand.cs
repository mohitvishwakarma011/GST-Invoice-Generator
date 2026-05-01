using MediatR;

namespace GI.Application.Features.Clients.DeleteClient
{
    public record DeleteClientCommand(int ClientId, int UserId) : IRequest;
}
