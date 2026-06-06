namespace GI.Application.DataTransferObjects.Auth
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiry { get; set; }
        public string Email { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
    }
}
