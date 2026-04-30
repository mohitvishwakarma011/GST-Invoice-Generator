using GI.Application.DataTransferObjects.Auth;
using MediatR;

namespace GI.Application.Features.Auth.Commands.Login
{
   public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;
}
