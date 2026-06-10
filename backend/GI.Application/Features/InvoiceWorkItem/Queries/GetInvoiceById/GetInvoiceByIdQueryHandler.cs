using AutoMapper;
using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicesForUser;
using GI.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery,InvoiceDetailDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ILogger<GetInvoiceByIdQueryHandler> _logger;
        public GetInvoiceByIdQueryHandler(IAppDbContext appDbContext, 
            IMapper mapper, IMemoryCache memoryCache,
            ILogger<GetInvoiceByIdQueryHandler> logger)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _cache = memoryCache;
            _logger = logger;
        }

        public async Task<InvoiceDetailDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var key = Helper.GetCachingKey(CachingKeyPrefix.InvoiceById, request.UserId, request.InvoiceId);
            if(_cache.TryGetValue(key,out InvoiceDetailDto cachedResult))
            {
                _logger.LogInformation($"Handled {nameof(GetInvoiceByIdQuery)} from Cache");
                return cachedResult!;
            }

            var invoice = await _appDbContext.Invoices.Include(x => x.Items).Include(x => x.Client).AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == request.InvoiceId && x.UserId == request.UserId && x.EntityStatus != EntityStatus.Deleted,cancellationToken);
            if (invoice is null) throw new KeyNotFoundException("Invoice does not found");

            _logger.LogInformation($"Handled {nameof(GetInvoiceByIdQuery)} from DB");

            var result = _mapper.Map<InvoiceDetailDto>(invoice);
            _cache.Set(key, result, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddHours(1),
                SlidingExpiration = TimeSpan.FromMinutes(20),
                Priority = CacheItemPriority.Normal,
                Size = 1,
            });
            return result;
        }
    }
}
