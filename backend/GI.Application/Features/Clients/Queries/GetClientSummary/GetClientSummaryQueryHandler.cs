using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.Client;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.Queries.GetClientSummary
{
    public class GetClientSummaryQueryHandler : IRequestHandler<GetClientSummaryQuery, ClientSummaryDto>
    {
        private readonly IAppDbContext _appDbContext;
        public GetClientSummaryQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ClientSummaryDto> Handle(GetClientSummaryQuery request, CancellationToken cancellationToken)
        {
            var summary = new ClientSummaryDto();

            var invoices = await _appDbContext.Invoices.AsNoTracking()
                            .Where(x => x.UserId == request.UserId && x.ClientId == request.ClientId).ToListAsync(cancellationToken);

            summary.TotalInvoices = invoices.Count();
            summary.TotalBilled = invoices.Sum(x => x.Total);
            summary.AmountPaid = invoices.Where(x => x.Status == InvoiceStatus.Paid).Sum(x => x.Total);
            summary.Outstanding = invoices.Where(x => x.Status != InvoiceStatus.Paid).Sum(x => x.Total);
            return summary;
        }
    }
}
