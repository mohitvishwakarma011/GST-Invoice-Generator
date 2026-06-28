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
                throw new InvalidOperationException("Invalid email or password.");

            //revoke all unrevoked refresh Token
            await _db.RefreshTokens.Where(x => x.IsRevoked == false && x.UserId == user.Id).ExecuteUpdateAsync(setters => setters.SetProperty(t => t.IsRevoked,true),ct);
            var refreshToken = _tokenService.GenerateRefreshToken(user);
            _db.RefreshTokens.Add(refreshToken);
            await _db.SaveChangesAsync(ct);

            return new AuthResponse
            {
                AccessToken = _tokenService.GenerateAccessToken(user),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiry = refreshToken.ExpiresAt,
                Email = user.Email,
                BusinessName = user.BusinessName
            };
        }
    }
}
