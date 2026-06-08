using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicesForUser
{
    public class GetInvoiceForUserQueryHandler : IRequestHandler<GetInvoicesForUserQuery, IList<InvoiceListDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetInvoiceForUserQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<InvoiceListDto>> Handle(GetInvoicesForUserQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Invoices.AsNoTracking().Where(x => x.UserId == request.UserId && x.EntityStatus != EntityStatus.Deleted).OrderByDescending(x => x.CreatedOn).
                Select(x => new InvoiceListDto
                {
                    ClientName = x.Client.Name,
                    CreatedOn = x.CreatedOn,
                    DueDate = x.DueDate,
                    Id = x.Id,
                    InvoiceNumber = x.InvoiceNumber,
                    Status = x.Status,
                    Total = x.Total
                }).ToListAsync(cancellationToken);
        }
    }
}
