using GI.Application.DataTransferObjects.Client;

namespace GI.Application.DataTransferObjects.InvoiceWorkItem
{
    public class InvoiceDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Cgst { get; set; }      // used when intra-state
        public decimal Sgst { get; set; }      // used when intra-state
        public decimal Igst { get; set; }      // used when inter-state
        public decimal Total { get; set; }
        public string? Notes { get; set; }
        public ClientDto Client { get; set; } = null!;
        public IList<InvoiceItemDto> Items { get; set; } = [];
    }
}
