namespace GI.Application.DataTransferObjects.Dashboard
{
    public class RecentInvoiceItem
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = null!;
        public string ClientName { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
