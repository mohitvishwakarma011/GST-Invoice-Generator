using GI.Application.DataTransferObjects.Auth;
using MediatR;

namespace GI.Application.Features.Auth.Commands.Refresh
{
    public class RefreshAccessTokenCommand : IRequest<AuthResponse>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
