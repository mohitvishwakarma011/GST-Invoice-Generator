using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetRecentInvoiceItem
{
    public class GetRecentInvoiceItemQueryHandler : IRequestHandler<GetRecentInvoiceItemQuery, IList<RecentInvoiceItem>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetRecentInvoiceItemQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<RecentInvoiceItem>> Handle(GetRecentInvoiceItemQuery request, CancellationToken cancellationToken)
        {
            var query = _appDbContext.Invoices.AsNoTracking().Include(x => x.Client).Where(x => x.UserId == request.UserId).OrderByDescending(x => x.CreatedOn);
                return await query.Select(x => new RecentInvoiceItem{
                    Amount = x.Total,
                    ClientName = x.Client.Name,
                    DueDate = x.DueDate,
                    Id = x.Id,
                    InvoiceNumber = x.InvoiceNumber,
                    Status = x.Status
                }).ToListAsync(cancellationToken);
        }
    }
}
