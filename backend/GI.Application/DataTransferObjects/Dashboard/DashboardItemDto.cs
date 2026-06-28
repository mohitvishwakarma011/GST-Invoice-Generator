namespace GI.Application.DataTransferObjects.Dashboard
{
    public class DashboardItemDto
    {
        public string Type { get; set; } = null!;
        public int Count { get; set; }
        public string Description { get; set; } = null!;
    }

    public class DashboardSummaryDto
    {
        public IList<DashboardItemDto> dashboardItems { get; set; } = [];
        public OverallMonthSummaryDto OverallMonthSummary { get; set; } = null!;
    }

    public class OverallMonthSummaryDto
    {
        public int InvoiceRaised { get; set; }
        public decimal AmountBilled { get; set; }
        public decimal AmountReceived { get; set; }
        public decimal GstCollected { get; set; }
        public int ActiveClients { get; set; }
    }
}
