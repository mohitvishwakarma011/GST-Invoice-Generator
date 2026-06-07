namespace GI.Application.DataTransferObjects.InvoiceWorkItem
{
    public class InvoiceItemDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string? HsnCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
