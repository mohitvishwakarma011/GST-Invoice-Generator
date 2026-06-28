namespace GI.Application.DataTransferObjects.InvoiceWorkItem
{
    public class DashboardItemDto
    {
        public string Type { get; set; } = null!;
        public int Count { get; set; }
        public string Description { get; set; } = null!;
    }
}
