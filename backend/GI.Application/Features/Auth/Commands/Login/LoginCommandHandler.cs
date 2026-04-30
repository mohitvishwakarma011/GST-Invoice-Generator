using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand,AuthResponse>
    {
        private readonly IAppDbContext _db;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IAppDbContext appDbContext, ITokenService tokenService)
        {
            _db = appDbContext;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken ct)
        {
            var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email.ToLower(), ct);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return new AuthResponse
            {
                Token = _tokenService.GenerateToken(user),
                Email = user.Email,
                BusinessName = user.BusinessName
            };
        }
    }
}
