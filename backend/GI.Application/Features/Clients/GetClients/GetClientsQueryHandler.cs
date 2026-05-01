using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.GetClients
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IList<ClientDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetClientsQueryHandler(IAppDbContext appDbContext) => _appDbContext = appDbContext;
        public async Task<IList<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Clients
            .Where(c => c.UserId == request.UserId)
            .OrderByDescending(c => c.CreatedOn)
            .Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Gstin = c.Gstin,
                Email = c.Email,
                Address = c.Address,
                State = c.State,
                CreatedAt = c.CreatedOn
            })
            .ToListAsync(cancellationToken);
        }
    }
}
