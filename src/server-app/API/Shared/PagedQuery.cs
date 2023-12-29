using System.Web;

public record PagedQueryRequest(int Page=1, int PageSize=25, string SortBy="", string SortDirection="", string Filters="");
public class PagedQuery<T> where T : BaseListFilter
{
    public PagedQuery(PagedQueryRequest request, T filters)
    {
        Page = request.Page;
        PageSize = request.PageSize;
        SortBy = request.SortBy;
        SortDirection = request.SortDirection;
        Filters = filters;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public T Filters { get; set; }
    public string SortBy { get; set; }
    public string SortDirection { get; set; }
    public static bool TryParse(string? value, IFormatProvider? provider, out PagedQuery<T> pagedQuery)
    { 
        pagedQuery = new PagedQuery<T>(new PagedQueryRequest(), default(T)!);
        
        if (value == null)
        {
            return true;
        }
        
        var query = HttpUtility.ParseQueryString(value);
        if (query == null)
        {
            return true;
        }

        if (query["page"] != null)
        {
            pagedQuery.Page = int.Parse(query["page"]);
        }
        if (query["pageSize"] != null)
        {
            pagedQuery.PageSize = int.Parse(query["pageSize"]);
        }
        if (query["sortBy"] != null)
        {
            pagedQuery.SortBy = query["sortBy"];
        }
        if (query["sortDirection"] != null)
        {
            pagedQuery.SortDirection = query["sortDirection"];
        }
    //     if (query.AllKeys.Any(x=>x.Contains("filters[")))
    //     {
    //         var filtersKeys = query.AllKeys.Where(x => x.Contains("filters["));
    //         var filtersString = string.Join("&", filtersKeys.Select(x => $"{x}={query[x]}"));

    //         //var filters = (BaseListFilter)default(T)!;
    //         //typeof(T).GetMethod("TryParse", new Type[] { typeof(string), typeof(IFormatProvider), typeof(T).MakeByRefType() })?
    //         //.Invoke(null, new object[] { filtersString, provider, filters });
    //         _ = NoteListFilter.TryParse(filtersString, provider, out var filters);
    //         pagedQuery.Filters = filters as T;
        
    // }

        return true;
    }
}