
using Microsoft.AspNetCore.Mvc;

public static class NotesEndpoints{
    
private static List<Note> notes = new(){new Note( 1, "Item1", false ), new Note( 2, "Item2", false )};


    public static void MapNotesEndpoints(this IEndpointRouteBuilder app)
    {
        
        //http://localhost:5271/api/notes2?page=1&pageSize=25&filters=title%3D2%26completed%3Dtrue
        app.MapGet("/api/notes2", (HttpContext context,[AsParameters]PagedQueryRequest req) => {
            var pagedQuery = new PagedQuery<NoteListFilter>(req, NoteListFilter.TryParse(req.Filters, null, out var filters) ? filters : default(NoteListFilter)!);

            var filteredItems = notes
                .Where(x => pagedQuery.Filters?.Title == null || x.Title.Contains(pagedQuery.Filters.Title))
                .Where(x => pagedQuery.Filters?.Completed == null || x.Completed == pagedQuery.Filters.Completed);
            var totalCount = filteredItems.Count();
            var pagedItems = filteredItems.Skip((pagedQuery.Page - 1) * pagedQuery.PageSize).Take(pagedQuery.PageSize).ToList();
            return  new PagedResult<Note>(pagedItems,totalCount, pagedQuery.Page, pagedQuery.PageSize, req.SortBy, req.SortDirection);
        });

        app.MapGet("/api/notes", (PagedQuery<NoteListFilter> pagedQuery) => {
            var filteredItems = notes
                .Where(x => pagedQuery.Filters?.Title == null || x.Title.Contains(pagedQuery.Filters.Title))
                .Where(x => pagedQuery.Filters?.Completed == null || x.Completed == pagedQuery.Filters.Completed);
            var totalCount = filteredItems.Count();
            var pagedItems = filteredItems.Skip((pagedQuery.Page - 1) * pagedQuery.PageSize).Take(pagedQuery.PageSize).ToList();
            return  new PagedResult<Note>(pagedItems, pagedQuery.Page, pagedQuery.PageSize, totalCount);
        });
        app.MapGet("/api/notes/{id}", (int id) => notes.Where(x => x.Id == id).FirstOrDefault());
        app.MapPost("/api/notes", (Note Note) =>
        {
            notes.Add(Note);
            return Note;
        });
        app.MapDelete("/api/notes/{id}", (int id) =>
        {
            var Note = notes.Where(x => x.Id == id).FirstOrDefault();
            notes.Remove(Note);
            return Note;
        });
    }
}
