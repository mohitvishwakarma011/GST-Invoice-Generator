using AutoMapper;
using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.UserItem
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserDetailDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public GetUserInfoQueryHandler(IAppDbContext appDbContext,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<UserDetailDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.UserId);

            return _mapper.Map<UserDetailDto>(user);
        }
    }
}
