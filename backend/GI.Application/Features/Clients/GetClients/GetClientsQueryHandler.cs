using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;

namespace GI.Application.Features.Clients.GetClients
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IList<ClientDto>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMemoryCache _cache;
        private readonly ILogger<GetClientsQueryHandler> _logger;
        public GetClientsQueryHandler(IAppDbContext appDbContext, IMemoryCache cache, ILogger<GetClientsQueryHandler> logger)
        {
            _cache = cache;
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IList<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            request.AssignDefaultValues("CreatedOn");

            var cacheKey = $"uid:{request.UserId}_ps:{request.PageSize}_pi:{request.PageIndex}_s:{request.Sort}_o:{request.Order}_sch:{request.Search}";

            //Check Cache if data exist
            if(_cache.TryGetValue(cacheKey,out IList<ClientDto> result)){
                _logger.LogInformation($"Handled {nameof(GetClientsQuery)} from cache");
                return result!;
            }

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

            var unCachedResult = await dbQuery.Select(c => new ClientDto
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

            _cache.Set(cacheKey, unCachedResult, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(5),
                Priority = CacheItemPriority.Normal,

            });

            _logger.LogInformation($"Handled {nameof(GetClientsQuery)} from DB");
            return unCachedResult;
        }
    }
}
