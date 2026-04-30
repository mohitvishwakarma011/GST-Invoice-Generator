namespace GI.Core.Entities
{
    public class Invoice : BaseAudit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

        public decimal Subtotal { get; set; }
        public decimal Cgst { get; set; }      // used when intra-state
        public decimal Sgst { get; set; }      // used when intra-state
        public decimal Igst { get; set; }      // used when inter-state
        public decimal Total { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Client Client { get; set; } = null!;
        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }
}
