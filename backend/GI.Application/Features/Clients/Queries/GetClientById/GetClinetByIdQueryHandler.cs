using AutoMapper;
using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.Queries.GetClientById
{
    public class GetClinetByIdQueryHandler : IRequestHandler<GetClinetByIdQuery, ClientDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public GetClinetByIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<ClientDto> Handle(GetClinetByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _appDbContext.Clients.AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.ClientId && x.UserId == request.UserId);
            return _mapper.Map<ClientDto>(client);
        }
    }
}
