using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Stripboekensite.Pages
{
    [Authorize(Roles = Gebruiker.GebruikersRollen.Moderator)]
    public class BoekToevoegenModel : PageModel
    {
        public List<Genre> Genres = new List<Genre>();
        public Stripboek stripboek = new Stripboek();
        public List<Uitgever> uitgevers = new List<Uitgever>();
        public List<Reeks> reeksen = new List<Reeks>();
        public List<Creator> creators = new List<Creator>();
        //public List<int> genreids { get; set; }

        //id of current user
        public int userid;
        
        public void OnGet()
        {
            setlists();
            //gets id from cookie

        }
        
        public void OnPostAddStripboek(string titel, string isbn, int jaar1druk,string uitgever, int expleciet, int paginas, string reeksnaam, int reeksnum,string schrijvernaam, string tekenaarnaam, List<int> genreids)
        {
            setlists();
            //sets al variabels of stripboek
            Stripboek addedstripboek = new Stripboek();
            stripboek.titel = titel;
            stripboek.isbn = isbn;
            stripboek.Uitgave1e_druk = jaar1druk;
            stripboek.expleciet = expleciet;
            stripboek.Bladzijden = paginas;
            stripboek.Reeks_nr = reeksnum;

            foreach (var uitgevr in uitgevers)
            {
                if (uitgevr.Naam.ToLower() == uitgever.ToLower())
                {
                    stripboek.Uitgever_id = uitgevr.Uitgever_id;
                }
            }
            if (stripboek.Uitgever_id == 0)
            {
                //creates a uitgever connection to stripboek if it does not exist
                Uitgever newuitgever = new Uitgever();
                newuitgever.Naam = uitgever;
                Uitgever newruitgever = new UitgeverRepository().Add(newuitgever);
                stripboek.Uitgever_id = newruitgever.Uitgever_id;
            }
            
            if (reeksnaam != null)
            {
                
                foreach (var reeks in reeksen)
                {
                    if (reeks.Reeks_titel.ToLower() == reeksnaam.ToLower())
                    {
                        stripboek.Reeks_id = reeks.Reeks_id;
                        break;
                    }
                }

                if (stripboek.Reeks_id == 0)
                {
                    //checks whether there is a reeks and will add with reeks 
                    Reeks newrReeks = new Reeks();
                    newrReeks.Reeks_titel = reeksnaam;
                    stripboek.Reeks_id = new ReeksRepository().Add(newrReeks).Reeks_id;
                }

            }

            addedstripboek = new StripboekRepository().Add(stripboek);   
           
            //adds stripboek gebruiker connection to database
            Gebruikers_Stripboeken stripboekgebruiker = new Gebruikers_Stripboeken();
            stripboekgebruiker.Gebruiker_id = userid;
            stripboekgebruiker.stripboek_id = addedstripboek.Stripboek_id;
            new Gebruikers_StripboekenRepository().Add(stripboekgebruiker);

            if (schrijvernaam != null )
            {
                taakcreator(schrijvernaam, "schrijver", addedstripboek);
            }

            if (tekenaarnaam != null)
            {
                taakcreator(tekenaarnaam, "tekenaar", addedstripboek);

            }
            
            List<GenreStripboek> genrelist = new List<GenreStripboek>();
            foreach (var genreid in genreids)
            {
                //adds a genre stripboek connection to database
                GenreStripboek stripboekgenre = new GenreStripboek();
                stripboekgenre.Stripboek_id = addedstripboek.Stripboek_id;
                stripboekgenre.Genre_id = genreid;
                genrelist.Add(stripboekgenre);
                //new GenreStripboekRepository().Add(stripboekgenre);
            }

            new GenreStripboekRepository().Add(genrelist);
        }

        public void setlists()
        {
            //var claims =ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            List<Claim> claims = User.Claims.ToList();

            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {

                    userid = int.Parse(claim.Value);
                }
            }
            
            //set the genres so can be used all over the page
            Genres = new GenreRepository().Get().ToList();
            reeksen = new ReeksRepository().Get().ToList();
            uitgevers = new UitgeverRepository().Get().ToList();
            creators = new CreatorRepository().Get().ToList();
        }

        public void taakcreator(string naam, string taak, Stripboek stripboek)
        {
            CreatorStripboeken creatorstrip = new CreatorStripboeken();
            foreach (var creator in creators)
            {
                if (creator.Creator_naam.ToLower() == naam.ToLower())
                {
                    creatorstrip.Stripboek_id = stripboek.Stripboek_id;
                    creatorstrip.Creator_id = creator.Creator_id;
                    creatorstrip.taak = taak;
                    break;
                }
            }

            if (creatorstrip.Creator_id == 0)
            {
                Creator creator = new Creator();
                creator.Creator_naam = naam;
                Creator newcreator = new CreatorRepository().Add(creator);
                creators.Add(newcreator);
                
                creatorstrip.Stripboek_id = stripboek.Stripboek_id;
                creatorstrip.Creator_id = newcreator.Creator_id;
                creatorstrip.taak = taak;
            }
            new CreatorStripboekenRepository().Add(creatorstrip);
        }
        
        
    }
}
