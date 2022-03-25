using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

[Authorize(Roles = Gebruiker.GebruikersRollen.Moderator)]
public class BekijkCategorie : PageModel
{
    public List<Genre> Genres = new List<Genre>();

    public void OnGet()
    {
        GenreRepository genreRepository = new GenreRepository();
        Genres = genreRepository.Get().ToList();
    }

    public IActionResult OnPost(string soort, string genre_id)
    {
        Genre genre = new Genre();
        genre.genre_id = Int32.Parse(genre_id);
        genre.soort = soort;

        GenreRepository genreRepository = new GenreRepository();
        genreRepository.Update(genre);

        return RedirectToPage("BekijkCategorie");
    }
}