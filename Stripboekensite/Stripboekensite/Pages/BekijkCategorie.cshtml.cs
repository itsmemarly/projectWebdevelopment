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

    //if the update button is clicked update the genre with the info from the form
    public IActionResult OnPostUpdate(string soort, string genre_id)
    {
        Genre genre = new Genre();
        genre.genre_id = Int32.Parse(genre_id);
        genre.soort = soort;

        GenreRepository genreRepository = new GenreRepository();
        genreRepository.Update(genre);

        return RedirectToPage("BekijkCategorie");
    }

    public IActionResult OnPostDelete(string id)
    {
        int genreId = Int32.Parse(id);
        {
            //delete references to genre
            var genreStripboekRepository = new GenreStripboekRepository();
            genreStripboekRepository.DeleteByGenreID(genreId);
        }

        {
            //delete the genre
            var genreRepository = new GenreRepository();
            genreRepository.DeleteGenre(genreId);
        }
        //refresh the page
        return RedirectToPage("/BekijkCategorie");
    }
}