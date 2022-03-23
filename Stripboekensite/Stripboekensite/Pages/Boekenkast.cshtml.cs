using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Stripboekensite.Pages
{
    public class BoekenkastModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Gebruikers_Stripboeken> gebruikerlijst = new List<Gebruikers_Stripboeken>();
        public List<Genre> Genres = new List<Genre>();
        public List<GenreStripboek> GenreStripboeks = new List<GenreStripboek>();
        public List<GenreStripboek> stripboekgenreshowed = new List<GenreStripboek>();

        public List<Stripboek> searchresults = new List<Stripboek>();
        public List<GenreStripboek> ownedsearch = new List<GenreStripboek>();
        
        public int userid;
        
        public string message;
        public string querysearch;
        //int userid through cookies

        public void OnGet()
        {
            userbookcheck();
            //message = userid.ToString();
        }

        public void OnPostSearch(string search)
        {
            userbookcheck();
            
            //sets string query to %searchvalue% needed for the Like query  
            querysearch = "%" + search + "%";
           
            message = "je zoekt op :" + search;
            
            //checks if there even is a value in the database if false message will say no search results
            //if true added stripboek showed so the stripboek wil be showed on page
            searchresults = new StripboekRepository().GetSearch(querysearch).ToList();
            foreach (var stripboek in searchresults)
            {
                foreach (var stripboekuser in stripboekgenreshowed)
                {
                        
                    if (stripboek.Stripboek_id == stripboekuser.Stripboek.Stripboek_id)
                    {
                        ownedsearch.Add(stripboekuser);
                    }
                }
            }
                stripboekgenreshowed = new List<GenreStripboek>();
                stripboekgenreshowed = ownedsearch;

            if(ownedsearch.Count==0)
            {
                message = "geen zoek resultaat voor:" + search;
            }

        }

        public void OnPostDelete(int stripboekid)
        {
            userbookcheck();
            Gebruikers_Stripboeken gebruikersStripboeken = new Gebruikers_Stripboeken();
            gebruikersStripboeken = new Gebruikers_StripboekenRepository().Get(userid, stripboekid);
            if (new Gebruikers_StripboekenRepository().Delete(gebruikersStripboeken.Gebruiker_stripboek_ID))
            {
                userbookcheck();
            }

        }
        
        public void userbookcheck()
        {
            useridget();
            
            //gets all genres and the books that the current user has 
            Genres = new GenreRepository().Get().ToList();
            GenreStripboeks = new JoinRepository().joingenrestripboek().ToList();
            gebruikerlijst = new JoinRepository().joingebrstripboekstripboeken(userid).ToList();
            
            //checks whether the books in the genre are owned 
            foreach (var gebruikstrip in gebruikerlijst)
            {
                foreach (var genrestripboek in GenreStripboeks)
                {
                    if (gebruikstrip.Stripboek.Stripboek_id == genrestripboek.Stripboek.Stripboek_id)
                    {
                        stripboekgenreshowed.Add(genrestripboek);
                    }
                }
            }
        }

        public void useridget()
        {
            List<Claim> claims = User.Claims.ToList();

            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier)
                {
                    userid = int.Parse(claim.Value);
                }
            }
        }
    }
}
