using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetGroupedInvoiceList
{
    public class GetGroupedInvoiceListQueryHandler : IRequestHandler<GetGroupedInvoiceListQuery, IList<GroupedInvoiceItemDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetGroupedInvoiceListQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<GroupedInvoiceItemDto>> Handle(GetGroupedInvoiceListQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Invoices.Include(x => x.Client).AsNoTracking()
                            .Where(x => x.UserId == request.UserId && x.EntityStatus != EntityStatus.Deleted)
                            .GroupBy(x => x.Client.Name)
                            .Select(gi => new GroupedInvoiceItemDto
                            {
                                InvoiceList = gi.Select(x => new InvoiceListDto
                                {
                                    ClientName = x.Client.Name,
                                    CreatedOn = x.CreatedOn,
                                    DueDate = x.DueDate,
                                    Id = x.Id,
                                    InvoiceNumber = x.InvoiceNumber,
                                    Status = x.Status,
                                    Total = x.Total
                                }).ToList(),
                                ClientName = gi.Key,
                                TotalAmount = gi.Sum(x => x.Total),
                                TotalInvoices = gi.Count()
                            }).ToListAsync(cancellationToken);
        }
    }
}
