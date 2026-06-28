using MediatR;

namespace GI.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string BusinessName { get; set; } = null!;
        public string Gstin { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int StateId { get; set; }
    }
}
