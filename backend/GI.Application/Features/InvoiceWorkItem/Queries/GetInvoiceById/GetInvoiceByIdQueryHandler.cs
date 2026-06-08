using AutoMapper;
using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoiceById
{
    public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery,InvoiceDetailDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public GetInvoiceByIdQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<InvoiceDetailDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _appDbContext.Invoices.Include(x => x.Items).Include(x => x.Client).AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.InvoiceId && x.UserId == request.UserId && x.EntityStatus != EntityStatus.Deleted,cancellationToken);
            if (invoice is null) throw new KeyNotFoundException("Invoice does not found");
            return _mapper.Map<InvoiceDetailDto>(invoice);
        }
    }
}
