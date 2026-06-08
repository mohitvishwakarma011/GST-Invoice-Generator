using GI.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Commands.DeleteInvoice
{
    public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        public DeleteInvoiceCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
           var invoice = await _appDbContext.Invoices.SingleOrDefaultAsync(x => x.Id == request.InvoiceId && x.UserId == request.UserId,cancellationToken);
            if (invoice is null) throw new KeyNotFoundException("Invoice not found.");

            if (invoice.Status != InvoiceStatus.Draft)
                throw new InvalidOperationException("Only Draft invoices can be deleted.");

            invoice.EntityStatus = EntityStatus.Deleted;
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return invoice.Id;
        }
    }
}
