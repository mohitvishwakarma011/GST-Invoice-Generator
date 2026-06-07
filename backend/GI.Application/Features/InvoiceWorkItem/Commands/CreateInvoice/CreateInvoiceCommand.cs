using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Commands.CreateInvoice
{
    public class CreateInvoiceCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public DateTime DueDate { get; set; }
        public string? Notes { get; set; }
        public List<CreateInvoiceItemDto> Items { get; set; } = new();
    }

    public class CreateInvoiceItemDto
    {
        public string Description { get; set; } = string.Empty;
        public string? HsnCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
    }
}
