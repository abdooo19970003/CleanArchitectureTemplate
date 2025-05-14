namespace CleanArc.Application.DTOs
{
    public class QueryParameters
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<FilterCriteria> Filters { get; set; } = new();
        public QueryParameters()
        {
            // Default constructor
        }
        public int Skip => (PageNumber - 1) * PageSize;
        public int Take => PageSize;
        public QueryParameters(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
