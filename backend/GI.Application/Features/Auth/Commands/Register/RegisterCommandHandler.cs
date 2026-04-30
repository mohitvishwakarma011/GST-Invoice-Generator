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
                throw new InvalidOperationException("Email already registered.");

            var user = new User
            {
                Email = request.Email.ToLower().Trim(),
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                BusinessName = request.BusinessName,
                Gstin = request.Gstin.ToUpper().Trim(),
                Address = request.Address,
                State = request.State
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return new AuthResponse
            {
                Token = _tokenService.GenerateToken(user),
                Email = user.Email,
                BusinessName = user.BusinessName
            };
        }
    }
}
