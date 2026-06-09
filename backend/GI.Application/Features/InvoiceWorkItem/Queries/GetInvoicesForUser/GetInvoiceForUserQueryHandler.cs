using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


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
            request.AssignDefaultValues("CreatedOn");
            var query = _appDbContext.Invoices.Where(x => x.UserId == request.UserId && x.EntityStatus != EntityStatus.Deleted);

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => EF.Functions.Like(x.InvoiceNumber,request.Search));
            }

            query = query.OrderBy($"{request.Sort} {request.Order}");

            return await query.AsNoTracking()
                .Skip(request.RecordToSkip())
                .Take(request.PageSize)
                .Select(x => new InvoiceListDto
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
