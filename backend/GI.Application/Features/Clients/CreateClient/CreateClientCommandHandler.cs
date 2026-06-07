using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using GI.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
    {
        private readonly IAppDbContext _appDbContext;
        public CreateClientCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken ct)
        {
            var state = await _appDbContext.States.SingleOrDefaultAsync(x => x.Code == request.StateCode) ??
                        throw new InvalidOperationException("Invalid State Code");

            var client = new Client
            {
                UserId = request.UserId,
                Name = request.Name.Trim(),
                Gstin = request.Gstin?.ToUpper().Trim(),
                Email = request.Email.ToLower().Trim(),
                BillingAddress = request.BillingAddress.Trim(),
                ShippingAddress = request.ShippingAddress?.Trim(),
                State = state.Name,
                StateCode = state.Code,
                CreatedBy = request.UserId,
                CreatedOn = DateTime.UtcNow,
                EntityStatus = EntityStatus.Active
            };

            _appDbContext.Clients.Add(client);
            await _appDbContext.SaveChangesAsync(ct);

            return MapToDto(client);
        }

        private static ClientDto MapToDto(Client c) => new()
        {
            Id = c.Id,
            Name = c.Name,
            Gstin = c.Gstin,
            Email = c.Email,
            BillingAddress = c.BillingAddress,
            ShippingAddress = c.ShippingAddress,
            State = c.State,
            StateCode = c.StateCode,
            CreatedOn = c.CreatedOn
        };
    }
}
