namespace GI.Application.DataTransferObjects.InvoiceWorkItem
{
    public class CreateInvoiceDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string CustomerGstin { get; set; } = string.Empty;
        public int CountryOfSupply { get; set; }
        public int PlaceOfSupply { get; set; }
        public DateOnly DateOfSupply { get; set; }
    }
}
