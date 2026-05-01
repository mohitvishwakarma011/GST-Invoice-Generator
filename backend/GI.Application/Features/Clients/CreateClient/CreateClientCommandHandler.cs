using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using GI.Core.Entities;
using MediatR;

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
            var client = new Client
            {
                UserId = request.UserId,
                Name = request.Name.Trim(),
                Gstin = request.Gstin?.ToUpper().Trim(),
                Email = request.Email.ToLower().Trim(),
                Address = request.Address.Trim(),
                State = request.State.Trim()
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
            Address = c.Address,
            State = c.State,
            CreatedAt = c.CreatedOn
        };
    }
}
