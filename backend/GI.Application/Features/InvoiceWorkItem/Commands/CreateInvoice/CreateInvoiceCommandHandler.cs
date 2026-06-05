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
            var user = await _appDbContext.Users.SingleOrDefaultAsync(_ => _.Id == request.UserId);
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
            if(user.StateCode == client.StateCode)
            {
                invoice.Sgst = subTotal * Tax.StateTax;
                invoice.Cgst = subTotal * Tax.StateTax;
                invoice.Igst = 0;
            }
            else
            {
                invoice.Sgst = 0;
                invoice.Cgst = 0;
                invoice.Igst = subTotal * Tax.CentralTax;
            }
            invoice.Total = invoice.Subtotal + invoice.Cgst + invoice.Igst + invoice.Sgst;

            //Create InvoiceItem List
            var invoiceList = request.Items.Select(x => new InvoiceItem
            {
                InvoiceNumber = invoice.InvoiceNumber,
                Description = x.Description,
                HsnCode = x.HsnCode,
                Quantity = x.Quantity,
                Rate = x.Rate,
                Amount = x.Quantity * x.Rate,
            }).ToList();
            invoice.Items = invoiceList;
            _appDbContext.Invoices.Add(invoice);
            await _appDbContext.SaveChangesAsync();
            return new CreateInvoiceDto();
        }
    }
}
