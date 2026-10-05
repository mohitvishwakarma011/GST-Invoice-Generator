namespace GI.Application.DataTransferObjects.InvoiceWorkItem
{
    public class GroupedInvoiceItemDto
    {
        public IList<InvoiceListDto> InvoiceList { get; set; } = [];
        public decimal TotalAmount { get; set; }
        public int TotalInvoices { get; set; }
        public string ClientName { get; set; }  = string.Empty;
    }
}
