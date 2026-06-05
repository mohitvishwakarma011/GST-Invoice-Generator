namespace GI.Core.Utilities
{
    public static class Helper
    {
        public static string GetUniqueInvoiceNumber(int currentinvCount)
        {
            return $"INV-{(currentinvCount + 1):D4}";
        }
    }
}
