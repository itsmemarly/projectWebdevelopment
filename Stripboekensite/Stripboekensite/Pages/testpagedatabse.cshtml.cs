using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite;

public class testpagedatabse : PageModel
{
    public List<genre> Genres = new List<genre>();

    public void OnGet()
    {
        Genres = new GenreRepository().Get().ToList();
    }
}