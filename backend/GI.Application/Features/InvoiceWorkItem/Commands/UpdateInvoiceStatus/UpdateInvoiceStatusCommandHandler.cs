using GI.Application.Common.Interfaces;
using GI.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace GI.Application.Features.InvoiceWorkItem.Commands.UpdateInvoiceStatus
{
    public class UpdateInvoiceStatusCommandHandler : IRequestHandler<UpdateInvoiceStatusCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMemoryCache _cache;
        private readonly ILogger<UpdateInvoiceStatusCommandHandler> _logger;
        public UpdateInvoiceStatusCommandHandler(IAppDbContext appDbContext,ILogger<UpdateInvoiceStatusCommandHandler> logger,IMemoryCache cache)
        {
            _appDbContext = appDbContext;
            _cache = cache;
            _logger = logger;
        }

        public async Task<int> Handle(UpdateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _appDbContext.Invoices.SingleOrDefaultAsync(x => x.Id == request.InvoiceId && x.UserId == request.UserId, cancellationToken) ??
            throw new KeyNotFoundException("Invoice not found.");

            switch (request.NewStatus) {
                case InvoiceStatus.Sent:
                    if (invoice.Status != InvoiceStatus.Draft)
                        throw new InvalidOperationException($"Invoice cannot be moved from {invoice.Status} to Sent.");
                    invoice.Status = InvoiceStatus.Sent;
                    break;
                case InvoiceStatus.Paid:
                    if (invoice.Status != InvoiceStatus.Sent)
                        throw new InvalidOperationException($"Invoice cannot be moved from {invoice.Status} to Paid.");
                    invoice.Status = InvoiceStatus.Paid;
                    break;
                default: throw new InvalidOperationException("Invalid status operation.");
            }
            await _appDbContext.SaveChangesAsync(cancellationToken);
            var key = Helper.GetCachingKey(CachingKeyPrefix.InvoiceById, request.UserId, request.InvoiceId);
            _cache.Remove(key);
            _logger.LogInformation($"Key-{key} has been removed from cache");

            return invoice.Id;
        }
    }
}
