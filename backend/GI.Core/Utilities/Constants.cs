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