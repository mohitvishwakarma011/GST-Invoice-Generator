using GI.Application.Common.Interfaces;
using GI.Core.Entities;
using GI.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, int>
    {
        private readonly IAppDbContext _appDbContext;
        public CreateInvoiceCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<int> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            //Check whether the client exists
            var client = await _appDbContext.Clients.SingleOrDefaultAsync(x => x.Id == request.ClientId && x.UserId == request.UserId, cancellationToken);
            var user = await _appDbContext.Users.SingleOrDefaultAsync(_ => _.Id == request.UserId,cancellationToken);

            if (client == null)
                throw new KeyNotFoundException("Client does not exist");
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var invoiceCount = await _appDbContext.Invoices.CountAsync(cancellationToken);
            var invoice = new Invoice
            {
                ClientId = request.ClientId,
                UserId = request.UserId,
                InvoiceNumber = Helper.GetUniqueInvoiceNumber(invoiceCount),
                DueDate = request.DueDate,
                Notes = string.IsNullOrEmpty(request.Notes) ? null: request.Notes,
                CreatedBy = request.UserId
            };
            invoice.Subtotal = request.Items.Sum(x => x.Quantity * x.Rate);
            if(user!.StateCode == client.StateCode)
            {
                invoice.Sgst = invoice.Subtotal * Tax.StateTax;
                invoice.Cgst = invoice.Subtotal * Tax.StateTax;
                invoice.Igst = 0;
            }
            else
            {
                invoice.Sgst = 0;
                invoice.Cgst = 0;
                invoice.Igst = invoice.Subtotal * Tax.CentralTax;
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
                CreatedBy = request.UserId
            }).ToList();
            invoice.Items = invoiceList;
            _appDbContext.Invoices.Add(invoice);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return invoice.Id;
        }
    }
}
