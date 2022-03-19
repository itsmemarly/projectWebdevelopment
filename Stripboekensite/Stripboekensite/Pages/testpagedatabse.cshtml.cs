using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite;

public class testpagedatabse : PageModel
{
    public List<Genre> Genres = new List<Genre>();
    public string message;

    public void OnGet()
    {
        Genres = new GenreRepository().Get().ToList();
    }

    //test voor user toevoeging tot de database
    public void OnPostCreate(string naam)
    {
        if (new UitgeverRepository().checkid(1))
        {
            message = "true";
        }
    }
}