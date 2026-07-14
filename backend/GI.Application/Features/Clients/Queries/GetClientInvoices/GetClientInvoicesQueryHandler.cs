using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.Clients.Queries.GetClientInvoices
{
    public class GetClientInvoicesQueryHandler : IRequestHandler<GetClientInvoicesQuery, IList<InvoiceListDto>>
    {
        private readonly IAppDbContext _appDbContext;
        public GetClientInvoicesQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IList<InvoiceListDto>> Handle(GetClientInvoicesQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Invoices.AsNoTracking()
                        .Where(x => x.UserId == request.UserId && x.ClientId == request.ClientId)
                            .Select(x => new InvoiceListDto
                            {
                                InvoiceNumber = x.InvoiceNumber,
                                CreatedOn = x.CreatedOn,
                                DueDate = x.DueDate,
                                Total = x.Total,
                                Status = x.Status,
                                Id = x.Id
                            }).ToListAsync(cancellationToken);
        }
    }
}
