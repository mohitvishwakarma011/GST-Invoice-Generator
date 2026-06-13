using GI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
    {
        private readonly IAppDbContext _appDbContext;
        public UpdateClientCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _appDbContext.Clients.SingleOrDefaultAsync(x => x.Id == request.ClientId && x.UserId == request.UserId, cancellationToken) ??
                            throw new KeyNotFoundException("Invalid client look up.");
            if (request.StateCode != client.StateCode)
            {
                var state = await _appDbContext.States.SingleOrDefaultAsync(x => x.Code == request.StateCode, cancellationToken) ??
                            throw new KeyNotFoundException("Invalid state code.");
                client.State = state.Name;
                client.StateCode = state.Code;
            }

            if (request.Gstin != client.Gstin)
            {
                var exists = await _appDbContext.Clients.AnyAsync(x => x.UserId == request.UserId && x.Gstin == request.Gstin, cancellationToken);
                if (exists)
                {
                    throw new InvalidOperationException("A client with this GSTIN already exists.");
                }
                else
                    client.Gstin = request.Gstin;
            }

            if (request.Email != client.Email)
            {
                var exists = await _appDbContext.Clients.AnyAsync(x => x.UserId == request.UserId && x.Email == request.Email, cancellationToken);
                if (exists)
                {
                    throw new InvalidOperationException("A client with this email already exists.");
                }
                else
                    client.Email = request.Email;
            }

            client.Name = request.Name;
            client.BillingAddress = request.BillingAddress;
            client.ShippingAddress = request.ShippingAddress;
            client.UpdatedBy = request.UserId;
            client.UpdatedOn = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
