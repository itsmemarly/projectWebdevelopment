using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite;

public class testpagedatabse : PageModel
{
    public List<Genre> Genres = new List<Genre>();

    public void OnGet()
    {
        Genres = new GenreRepository().Get().ToList();
    }
}