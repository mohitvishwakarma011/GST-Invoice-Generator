using GI.Application.Common.Interfaces;
using GI.Application.Features.InvoiceWorkItem.Commands.UpdateInvoiceStatus;
using GI.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace GI.Application.Features.InvoiceWorkItem.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMemoryCache _cache;
        private readonly ILogger<DeleteInvoiceCommandHandler> _logger;

        public DeleteInvoiceCommandHandler(IAppDbContext appDbContext, ILogger<DeleteInvoiceCommandHandler> logger, IMemoryCache cache)
        {
            _appDbContext = appDbContext;
            _cache = cache;
            _logger = logger;
        }
        public async Task<int> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
           var invoice = await _appDbContext.Invoices.SingleOrDefaultAsync(x => x.Id == request.InvoiceId && x.UserId == request.UserId,cancellationToken);
            if (invoice is null) throw new KeyNotFoundException("Invoice not found.");

            if (invoice.Status != InvoiceStatus.Draft)
                throw new InvalidOperationException("Only Draft invoices can be deleted.");

            invoice.EntityStatus = EntityStatus.Deleted;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            var key = Helper.GetCachingKey(CachingKeyPrefix.InvoiceById, request.UserId, request.InvoiceId);
            _cache.Remove(key);
            _logger.LogInformation($"Key-{key} has been removed from cache");
            return invoice.Id;
        }
    }
}
