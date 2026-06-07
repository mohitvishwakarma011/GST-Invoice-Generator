using GI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.DeleteClient
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly IAppDbContext _appDbContext;
        public DeleteClientCommandHandler(IAppDbContext appDbContext) =>  _appDbContext = appDbContext;
        public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _appDbContext.Clients.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == request.ClientId, cancellationToken);

            if (client is null)
            {
                throw new KeyNotFoundException("Client does not exist.");
            }
            client.EntityStatus = EntityStatus.Deleted;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
