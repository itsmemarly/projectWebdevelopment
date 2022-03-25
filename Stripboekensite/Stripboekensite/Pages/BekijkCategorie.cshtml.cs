using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

[Authorize(Roles = "Moderator")]
public class BekijkCategorie : PageModel
{
    public List<Genre> Genres = new List<Genre>();
    public void OnGet()
    {
        GenreRepository genreRepository = new GenreRepository();
        Genres = genreRepository.Get().ToList();
    }
}