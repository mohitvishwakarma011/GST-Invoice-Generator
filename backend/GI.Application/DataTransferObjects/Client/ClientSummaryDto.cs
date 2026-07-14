namespace GI.Application.DataTransferObjects.Client
{
    public class ClientSummaryDto
    {
        public decimal Outstanding {  get; set; }
        public decimal AmountPaid { get; set; }
        public decimal TotalBilled { get; set; }
        public int TotalInvoices { get; set; }
    }
}
