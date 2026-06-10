namespace GI.Core.Utilities
{
    public static class Helper
    {
        public static string GetUniqueInvoiceNumber(int currentinvCount)
        {
            return $"INV-{(currentinvCount + 1):D4}";
        }

        public static string GetCachingKey(string requestName,int userId,object? specKey)
        {
            return (specKey is null) ? $"{requestName}_{userId}" : $"{requestName}_{userId}_{specKey.ToString()}";
        }
    }
}
