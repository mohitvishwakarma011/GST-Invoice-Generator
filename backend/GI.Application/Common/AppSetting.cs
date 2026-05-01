namespace GI.Application.Common
{
    public class AppSetting
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
}
