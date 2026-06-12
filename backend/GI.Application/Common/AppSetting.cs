namespace GI.Application.Common
{
    public static class AppSettings
    {
        public class JwtSetting
        {
            public const string SectionName = "JwtSettings";
            public string Secret { get; set; } = null!;
            public string Issuer { get; set; } = null!;
            public string Audience { get; set; } = null!;
            public int ExpiryTime { get; set; }
        }
    }

    public class EmailSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}
