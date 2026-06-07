namespace GI.Application.DataTransferObjects.InvoiceWorkItem
{
    public class InvoiceListDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime DueDate { get; set; }
        public InvoiceStatus Status { get; set; }
        public decimal Total { get; set; }
    }
}
