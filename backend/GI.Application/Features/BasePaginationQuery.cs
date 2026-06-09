namespace GI.Application.Features
{
    public class BasePaginationQuery
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public string Sort { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public string Search { get; set; } = string.Empty;
        public BasePaginationQuery()
        {
            PageSize = 10;
            PageIndex = 0;
            Order = SortOrder.Ascending;
        }

        public void AssignDefaultValues(string Sort)
        {
            if (PageSize <= 0) PageSize = 10;
            if (PageIndex < 0) PageIndex = 0;
            if ((Order != SortOrder.Descending.ToString()) && (Order != SortOrder.Ascending.ToString())){
                Order = SortOrder.Ascending;
            }
            if (string.IsNullOrEmpty(this.Sort)) this.Sort = Sort;
        }

        public int RecordToSkip()
        {
            return PageSize * PageIndex;
        }
    }
}