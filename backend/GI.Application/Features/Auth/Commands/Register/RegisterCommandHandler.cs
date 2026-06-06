using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Auth;
using GI.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly ITokenService _tokenService;

        public RegisterCommandHandler(IAppDbContext appDbContext, ITokenService tokenService)
        {
            _appDbContext = appDbContext;
            _tokenService = tokenService;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
         {
            bool emailExists = await _appDbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
            if (emailExists)
                throw new InvalidOperationException("An account with this email already exists.");
            var state = (await _appDbContext.States.SingleOrDefaultAsync(x => x.Code == request.StateId, cancellationToken)) ?? 
                         throw new KeyNotFoundException("Invalid state code.");
            var user = new User
            {
                Email = request.Email.ToLower().Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                BusinessName = request.BusinessName,
                Gstin = request.Gstin.ToUpper().Trim(),
                Address = request.Address,
                StateCode = state.Code,
                State = state.Name
            };

            _appDbContext.Users.Add(user);

            var refreshToken = _tokenService.GenerateRefreshToken(user);
            _appDbContext.RefreshTokens.Add(refreshToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

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
