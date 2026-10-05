using AutoMapper;
using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using GI.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public CreateClientCommandHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken ct)
        {
            var state = await _appDbContext.States.SingleOrDefaultAsync(x => x.Code == request.StateCode) ??
                        throw new InvalidOperationException("Invalid State Code");

            var clientExists = await _appDbContext.Clients.FirstOrDefaultAsync(x => x.UserId == request.UserId && ((request.Gstin != null && x.Gstin == request.Gstin) || x.Email == request.Email),ct);
            if (clientExists is not null)
            {
                if(clientExists.Email == request.Email)
                    throw new InvalidOperationException("A client with this email already exists.");

                throw new InvalidOperationException("Client with this GSTIN already exists.");
            }

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

            return _mapper.Map<ClientDto>(client);
        }
    }
}
