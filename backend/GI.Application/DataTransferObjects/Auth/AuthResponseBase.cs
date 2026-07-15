namespace GI.Application.DataTransferObjects.Auth
{
    public class AuthResponseBase
    {
        public string Email { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }
}
