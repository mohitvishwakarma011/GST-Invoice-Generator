using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Auth;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Auth.Commands.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, AuthResponse>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ITokenService _tokenService;
        public RefreshTokenCommandHandler(IAppDbContext appDbContext, ITokenService tokenService)
        {
            _appDbContext = appDbContext;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var token = await _appDbContext.RefreshTokens.Include(x=>x.User).SingleOrDefaultAsync(x => x.Token == request.RefreshToken);

            if(token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
                throw new InvalidOperationException("Invalid refresh token.");

            token.IsRevoked = true;
            if (token.User == null) throw new InvalidOperationException("User not found for refresh token");

            var newRefreshToken = _tokenService.GenerateRefreshToken(token.User);
            _appDbContext.RefreshTokens.Add(newRefreshToken);
            await _appDbContext.SaveChangesAsync();

            return new AuthResponse
            {
                AccessToken = _tokenService.GenerateAccessToken(token.User),
                RefreshToken = token.Token,
                RefreshTokenExpiry = token.ExpiresAt,
                Email = token.User.Email,
                BusinessName = token.User.BusinessName
            };
        }
    }
}
