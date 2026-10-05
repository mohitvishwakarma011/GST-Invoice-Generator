using MediatR;

namespace GI.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest
    {
        public int UserId { get; set; }
    }
}
