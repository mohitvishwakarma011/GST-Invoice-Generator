namespace GI.Core.Entities
{
    public class InvoiceItem : BaseAudit
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string? HsnCode { get; set; }    // HSN/SAC code for the service
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }     // Quantity × Rate

        // Navigation
        public Invoice Invoice { get; set; } = null!;
    }
}
