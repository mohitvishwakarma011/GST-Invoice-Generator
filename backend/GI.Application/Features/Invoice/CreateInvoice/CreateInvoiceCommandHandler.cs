using GI.Application.Common.Interfaces;
using MediatR;

namespace GI.Application.Features.Invoice.CreateInvoice
{
    public class CreateInvoiceCommandHandler(IAppDbContext appDbContext) : IRequestHandler<CreateInvoiceCommand, int>
    {
        public Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}
