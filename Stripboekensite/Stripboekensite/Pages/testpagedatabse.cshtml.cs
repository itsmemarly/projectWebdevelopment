using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite;

public class testpagedatabse : PageModel
{
    public List<Genre> Genres = new List<Genre>();
    public Gebruiker test = new Gebruiker();
    public string message;

    public void OnGet()
    {
        Genres = new GenreRepository().Get().ToList();
    }

    //test voor user toevoeging tot de database
    public void OnPostCreate(string naam)
    {
        test.naam = naam;
        test.rol = "mod";
        test.versleuteld_wachtwoord = "test";
        test.Gebruikersnaam = "test2";
        Gebruiker gebruikertest = new GebruikerRepository().Add(test);
        if (test.naam == gebruikertest.naam)
        {
            message = "new user added with " + gebruikertest.Gebruikersnaam;
        }
    }
}