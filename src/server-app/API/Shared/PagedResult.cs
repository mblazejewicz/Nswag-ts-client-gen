public class PagedResult<T>
{
    public PagedResult(List<T> pagedItems,int totalCount, int page, int pageSize, string sortBy = "", string sortDirection = "")
    {
        Data = pagedItems;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        SortBy = sortBy;
        SortDirection = sortDirection;
    }

    public List<T> Data { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public string SortBy { get; set; }
    public string SortDirection { get; set; }

}
