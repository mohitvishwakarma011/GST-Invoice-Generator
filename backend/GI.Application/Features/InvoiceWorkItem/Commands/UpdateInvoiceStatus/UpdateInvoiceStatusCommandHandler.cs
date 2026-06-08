using GI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Commands.UpdateInvoiceStatus
{
    public class UpdateInvoiceStatusCommandHandler : IRequestHandler<UpdateInvoiceStatusCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        public UpdateInvoiceStatusCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
            return invoice.Id;
        }
    }
}
