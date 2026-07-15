namespace GI.Application.DataTransferObjects.Auth
{
    public class AuthResponse : AuthResponseBase
    {
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
