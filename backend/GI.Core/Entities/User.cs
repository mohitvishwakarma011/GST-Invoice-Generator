namespace GI.Core.Entities
{
    internal class User : BaseAudit
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string Gstin { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        public string? IfscCode { get; set; }
        public string? UpiId { get; set; }

        // Navigation
        public ICollection<Client> Clients { get; set; } = new List<Client>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
