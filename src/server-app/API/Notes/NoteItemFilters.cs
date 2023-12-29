using System.Web;

public class NoteListFilter : BaseListFilter
{
    public string Title { get; set; }
    public bool? Completed { get; set; }
    public int[] Ids { get; set; }
    
     public static bool TryParse(string? value, IFormatProvider? provider, out NoteListFilter filter)
    { 
        filter = new NoteListFilter();
        if (value == null)
        {
            return true;
        }
        //value = value.Replace("filters[", "").Replace("]", "");
        var query = HttpUtility.ParseQueryString(value);
        if (query["title"] != null)
        {
            filter.Title = query["title"];
        }
        if (query["completed"] != null)
        {
            filter.Completed = bool.Parse(query["completed"]);
        }
        if (query["ids"] != null)
        {
            filter.Ids = query["ids"].Split(',').Select(x=>int.Parse(x)).ToArray();
        }
        return true;
    }
}

public class BaseListFilter
{
    public static bool TryParse(string? value, IFormatProvider? provider, out BaseListFilter filter)
    {
        filter = default(BaseListFilter)!;
        return true;
    }

}