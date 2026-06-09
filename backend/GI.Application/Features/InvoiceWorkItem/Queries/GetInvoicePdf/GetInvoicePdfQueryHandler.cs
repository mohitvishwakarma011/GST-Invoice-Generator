using AutoMapper;
using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicePdf
{
    public class GetInvoicePdfQueryHandler : IRequestHandler<GetInvoicePdfQuery, InvoiceDetailForPdfDto>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public GetInvoicePdfQueryHandler(IAppDbContext appDbContext,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<InvoiceDetailForPdfDto> Handle(GetInvoicePdfQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _appDbContext.Invoices.Include(x => x.User).Include(x => x.Items).Include(x => x.Client).AsNoTracking().SingleOrDefaultAsync(x => x.Id == request.InvoiceId && x.UserId == request.UserId,cancellationToken) ??
                            throw new KeyNotFoundException("Invoice not found.");

            return _mapper.Map<InvoiceDetailForPdfDto>(invoice);
        }
    }
}
