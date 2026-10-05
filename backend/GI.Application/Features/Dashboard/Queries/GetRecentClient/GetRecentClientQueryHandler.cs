using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Dashboard;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Dashboard.Queries.GetRecentClient
{
    public class GetRecentClientQueryHandler : IRequestHandler<GetRecentClientQuery, IList<RecentClientDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetRecentClientQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<RecentClientDto>> Handle(GetRecentClientQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Clients.AsNoTracking().Where(x => x.UserId == request.UserId).OrderByDescending(x => x.CreatedOn).Take(5).
                    Select(x => new RecentClientDto
                    {
                        Gstin = x.Gstin??"No GSTIN",
                        Name = x.Name,
                        State = x.State,
                        Id = x.Id,
                    }).ToListAsync(cancellationToken);
        }
    }
}
