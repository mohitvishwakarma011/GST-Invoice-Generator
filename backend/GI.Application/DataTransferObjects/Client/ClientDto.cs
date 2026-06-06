namespace GI.Application.DataTransferObjects.Client
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Gstin { get; set; }
        public string Email { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string ShippingAddress { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int StateCode { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
