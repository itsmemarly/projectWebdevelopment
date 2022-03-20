using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages
{
    public class BoekToevoegenModel : PageModel
    {
        public List<Genre> Genres = new List<Genre>();
        public Stripboek stripboek = new Stripboek();
        
        //id of current user
        public int gebruikerid = 6;
        
        public void OnGet()
        {
            genresset();
            //gets id from cookie

        }
        
        public void OnPostAddStripboek(string titel, string isbn, int jaar1druk,string uitgever, int expleciet, int paginas, string reeksnaam, int reeksnum, int genreid)
        {
            genresset();
            //sets al variabels of stripboek
            Stripboek addedstripboek = new Stripboek();
            stripboek.titel = titel;
            stripboek.isbn = isbn;
            stripboek.Uitgave1e_druk = jaar1druk;
            stripboek.expleciet = expleciet;
            stripboek.Bladzijden = paginas;
            stripboek.Reeks_nr = reeksnum;
            
            //creates a uitgever connection to stripboek
            Uitgever newuitgever = new Uitgever();
            newuitgever.Naam = uitgever;
            stripboek.Uitgever_id = new UitgeverRepository().Add(newuitgever).Uitgever_id;


            if (reeksnaam != null)
            {
                //checks whether there is a reeks and will add with reeks 
                Reeks newrReeks = new Reeks();
                newrReeks.Reeks_titel = reeksnaam;
                Reeks newReeks = new ReeksRepository().Add(newrReeks);
                stripboek.Reeks_id = newReeks.Reeks_id;
                addedstripboek = new StripboekRepository().Add(stripboek);
            }
            else
            {
                addedstripboek = new StripboekRepository().AddWithoutReeks(stripboek);
            }
            
            //adds a genre stripboek connection to database
            GenreStripboek stripboekgenre = new GenreStripboek();
            stripboekgenre.Stripboek_id = addedstripboek.Stripboek_id;
            stripboekgenre.Genre_id = genreid;
            GenreStripboek addedGenreStripboek = new GenreStripboekRepository().Add(stripboekgenre);
            
            //adds stripboek gebruiker connection to database
            Gebruikers_Stripboeken stripboekgebruiker = new Gebruikers_Stripboeken();
            stripboekgebruiker.Gebruiker_id = gebruikerid;
            stripboekgebruiker.stripboek_id = addedstripboek.Stripboek_id;
            Gebruikers_Stripboeken addedstripboekgebruiker = new Gebruikers_StripboekenRepository().Add(stripboekgebruiker);
        }

        public void genresset()
        {
            //set the genres so can be used all over the page
            Genres = new GenreRepository().Get().ToList();
        }
        
    }
}
