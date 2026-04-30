namespace GI.Application.DataTransferObjects.Auth
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;    
        public string Gstin { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
    }
}
