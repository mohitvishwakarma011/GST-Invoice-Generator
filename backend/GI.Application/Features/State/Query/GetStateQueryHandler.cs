using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.State;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.StateItem.Query
{
    public class GetStateQueryHandler : IRequestHandler<GetStateQuery, IList<StateDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetStateQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IList<StateDto>> Handle(GetStateQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.States.AsNoTracking().Select(x => new StateDto
            {
                Code = x.Code, Name = x.Name,Description = x.Description??""
            }).OrderBy(x => x.Name).ToListAsync(cancellationToken);
        }
    }
}
