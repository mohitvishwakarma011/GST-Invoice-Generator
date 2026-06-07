using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace GI.Application.Features.Clients.GetClients
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IList<ClientDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetClientsQueryHandler(IAppDbContext appDbContext) => _appDbContext = appDbContext;
        public async Task<IList<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            request.AssignDefaultValues("CreatedOn");

            var dbQuery = _appDbContext.Clients
            .Where(c => c.UserId == request.UserId && c.EntityStatus != EntityStatus.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
            {
                dbQuery = dbQuery.Where(x => EF.Functions.Like(x.Name, $"%{request.Search}%")
                || EF.Functions.Like(x.Gstin, $"%{request.Search}%")
                || EF.Functions.Like(x.Email, $"%{request.Search}%")
                || EF.Functions.Like(x.BillingAddress, $"%{request.Search}%")
                || EF.Functions.Like(x.ShippingAddress, $"%{request.Search}%")
                || EF.Functions.Like(x.State, $"%{request.Search}%"));
            }
            dbQuery = dbQuery.OrderBy($"{request.Sort} {request.Order}");

            return await dbQuery.Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Gstin = c.Gstin,
                Email = c.Email,
                ShippingAddress = c.ShippingAddress??"",
                BillingAddress = c.BillingAddress,
                State = c.State,
                StateCode = c.StateCode,
                CreatedOn = c.CreatedOn
            }).Skip(request.RecordToSkip()).Take(request.PageSize).ToListAsync();
        }
    }
}
