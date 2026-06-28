using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetDashboardSummary
{
    public class GetDashboardSummaryQueryHandler : IRequestHandler<GetDashboardSummaryQuery, IList<DashboardItemDto>>
    {

        private readonly IAppDbContext _appDbContext;
        public GetDashboardSummaryQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IList<DashboardItemDto>> Handle(GetDashboardSummaryQuery request, CancellationToken cancellationToken)
        {
            var currentYearInitialDate = new DateTime(DateTime.UtcNow.Year, 1, 1);

            var totalInvoices = await _appDbContext.Invoices.AsNoTracking().CountAsync(x => x.UserId == request.UserId,cancellationToken);
            var unpaidInvoices = await _appDbContext.Invoices.AsNoTracking().CountAsync(x => x.UserId == request.UserId && x.Status != InvoiceStatus.Paid, cancellationToken);
            var paidInvoices = await _appDbContext.Invoices.AsNoTracking().CountAsync(x => x.UserId == request.UserId && x.Status == InvoiceStatus.Paid && x.CreatedOn >= currentYearInitialDate, cancellationToken);
            var totalRevenue = await _appDbContext.Invoices.AsNoTracking().Where(x => x.UserId == request.UserId).SumAsync(x => x.Total,cancellationToken);

            return new List<DashboardItemDto>
            {
                new DashboardItemDto
                {
                    Count = totalInvoices,
                    Description = "All time",
                    Type = DashboardItemType.TotalInvoices
                },
                new DashboardItemDto
                {
                    Count = unpaidInvoices,
                    Description = "Awaiting payment",
                    Type = DashboardItemType.UnpaidInvoices
                },
                new DashboardItemDto
                {
                    Count = paidInvoices,
                    Description = "This year",
                    Type = DashboardItemType.PaidInvoices
                },
                new DashboardItemDto
                {
                    Count = (int)totalRevenue,
                    Description = "This financial year",
                    Type = DashboardItemType.TotalRevenue
                }
            };
        }
    }
}
