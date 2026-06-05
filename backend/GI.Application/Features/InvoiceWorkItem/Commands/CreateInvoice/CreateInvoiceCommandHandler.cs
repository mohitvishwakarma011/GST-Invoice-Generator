using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using GI.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GI.Core.Utilities;

namespace GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CreateInvoiceDto>
    {
        private readonly IAppDbContext _appDbContext;
        public CreateInvoiceCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<CreateInvoiceDto> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            //Check whether the client exists
            var client = await _appDbContext.Clients.SingleOrDefaultAsync(x => x.Id == request.ClientId);
            if (client == null)
            {
                throw new KeyNotFoundException("Client does not exist");
            }
            var invoiceCount = await _appDbContext.InvoiceItems.CountAsync();
            var invoice = new Invoice
            {
                ClientId = request.ClientId,
                UserId = request.UserId,
                InvoiceNumber = Helper.GetUniqueInvoiceNumber(invoiceCount),
                DueDate = request.DueDate,
                Notes = request.Notes
            };
            var subTotal = request.Items.Sum(x => x.Quantity * x.Rate);
            return await Task.FromResult(new CreateInvoiceDto());
        }
    }
}
