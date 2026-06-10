using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using GI.Application.Features.Clients.GetClients;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Linq.Dynamic.Core;


namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicesForUser
{
    public class GetInvoiceForUserQueryHandler : IRequestHandler<GetInvoicesForUserQuery, IList<InvoiceListDto>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger _logger;
        public GetInvoiceForUserQueryHandler(IAppDbContext appDbContext,
            IMemoryCache memoryCache, ILogger<GetInvoiceForUserQueryHandler> logger)
        {
            _appDbContext = appDbContext;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<IList<InvoiceListDto>> Handle(GetInvoicesForUserQuery request, CancellationToken cancellationToken)
        {
            request.AssignDefaultValues("CreatedOn");
            var cacheKey = $"{nameof(GetInvoicesForUserQuery)}_uid:{request.UserId}_ps:{request.PageSize}_pi:{request.PageIndex}_s:{request.Sort}_o:{request.Order}_sch:{request.Search}";
            if (_memoryCache.TryGetValue(cacheKey,out IList<InvoiceListDto> cachedResult)) {
                _logger.LogInformation($"Handled {nameof(GetInvoicesForUserQuery)} from Cache");
                return cachedResult!;
            }

            var query = _appDbContext.Invoices.Where(x => x.UserId == request.UserId && x.EntityStatus != EntityStatus.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => EF.Functions.Like(x.InvoiceNumber.ToLower(),request.Search.ToLower())
                || EF.Functions.Like(x.User.BusinessName.ToLower(), request.Search.ToLower())
                || EF.Functions.Like(x.User.Address.ToLower(), request.Search.ToLower())
                || EF.Functions.Like(x.User.State.ToLower(), request.Search.ToLower())
                || EF.Functions.Like(x.Client.Name.ToLower(), request.Search.ToLower())
                || EF.Functions.Like(x.Client.Gstin.ToLower(), request.Search.ToLower())
                || EF.Functions.Like(x.Client.Email.ToLower(), request.Search.ToLower())
                || EF.Functions.Like(x.Client.BillingAddress.ToLower(), request.Search.ToLower())
                || EF.Functions.Like(x.Client.State.ToLower(), request.Search.ToLower())
                || EF.Functions.Like(x.Client.ShippingAddress.ToLower(), request.Search.ToLower())
                );
            }

            query = query.OrderBy($"{request.Sort} {request.Order}");

            var uncachedResult = await query.AsNoTracking()
                .Skip(request.RecordToSkip())
                .Take(request.PageSize)
                .Select(x => new InvoiceListDto
                {
                    ClientName = x.Client.Name,
                    CreatedOn = x.CreatedOn,
                    DueDate = x.DueDate,
                    Id = x.Id,
                    InvoiceNumber = x.InvoiceNumber,
                    Status = x.Status,
                    Total = x.Total
                }).ToListAsync(cancellationToken);

            _memoryCache.Set(cacheKey, uncachedResult, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(2), //User might get stale data
                SlidingExpiration = TimeSpan.FromMinutes(1),
                Priority = CacheItemPriority.Normal,
                Size = uncachedResult.Count,
            });
            _logger.LogInformation($"Handled {nameof(GetInvoicesForUserQuery)} from DB");
            return uncachedResult;
        }
    }
}
