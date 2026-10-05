using GI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Auth.Commands.HardResetPassword
{
    public class HardResetPasswordCommandHandler : IRequestHandler<HardResetPasswordCommand>
    {
        private readonly IAppDbContext _appDbContext;
        public HardResetPasswordCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Handle(HardResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email,cancellationToken) ??
                        throw new InvalidOperationException("User doesn't exist.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
