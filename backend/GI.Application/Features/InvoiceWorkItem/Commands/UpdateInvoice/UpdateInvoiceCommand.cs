using GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Commands.UpdateInvoice
{
    public class UpdateInvoiceCommand : IRequest<int>
    {
        public int InvoiceId { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        public List<UpdateInvoiceItemDto> Items { get; set; } = new();
    }

    public class UpdateInvoiceItemDto
    {
        public string Description { get; set; } = string.Empty;
        public string? HsnCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}
