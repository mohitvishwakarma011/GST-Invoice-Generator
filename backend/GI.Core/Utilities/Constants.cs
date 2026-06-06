public enum InvoiceStatus { Draft, Sent, Paid }
public struct SortOrder
{
    public const string Ascending = "asc";
    public const string Descending = "desc";
}

public struct Tax
{
    public const decimal StateTax = 0.09m;
    public const decimal CentralTax = 0.18m;
}

public struct JWT
{
    public const string JwtSetting = "JwtSettings";
    public const string SecretKey = "SecretKey";
    public const string Issuer = "Issuer";
    public const string Audience = "Audience";
    public const string AccessTokenExpiry = "AccessTokenExpiryMins";
    public const string RefreshTokenExpiry = "RefreshTokenExpiryDays";
}

public struct UserClaims
{
    public const string BusinessName = "businessName";
}