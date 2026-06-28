using MediatR;

namespace GI.Application.Features.Auth.Commands.HardResetPassword
{
    public class HardResetPasswordCommand : IRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
