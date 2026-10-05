using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.Queries.GetClientSelectOption
{
    public class GetClientSelectOptionQueryHandler : IRequestHandler<GetClientSelectOptionQuery, IList<ClientSelectOptionDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetClientSelectOptionQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<ClientSelectOptionDto>> Handle(GetClientSelectOptionQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Clients.AsNoTracking().Where(x => x.UserId == request.UserId && x.EntityStatus == EntityStatus.Active)
                            .OrderBy(x => x.Name)
                        .Select(x => new ClientSelectOptionDto
                        {
                            Id = x.Id,
                            Name = x.Name,
                            StateCode = x.StateCode,
                        }).ToListAsync(cancellationToken);
        }
    }
}
