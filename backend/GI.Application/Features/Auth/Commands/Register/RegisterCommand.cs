using GI.Application.DataTransferObjects.Auth;
using MediatR;

namespace GI.Application.Features.Auth.Commands.Register
{
    public record RegisterCommand(
        string Email,
        string Password,
        string BusinessName,
        string Gstin,
        string Address,
        string State
        ) : IRequest<AuthResponse>;
}
