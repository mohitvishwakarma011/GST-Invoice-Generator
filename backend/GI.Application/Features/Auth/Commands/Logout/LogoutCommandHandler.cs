using GI.Application.Common.Interfaces;
using GI.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        IAppDbContext _appDbContext;
        public LogoutCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _appDbContext.RefreshTokens.Where(x => x.IsRevoked == false && x.UserId == request.UserId).ExecuteUpdateAsync(setters => setters.SetProperty(t => t.IsRevoked, true), cancellationToken);
        }
    }
}
