using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Stripboekensite.Pages
{
    public class BoekenkastModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        //s
        public List<Gebruikers_Stripboeken> gebruikerlijst = new List<Gebruikers_Stripboeken>();
        public List<GenreStripboek> GenreStripboeks = new List<GenreStripboek>();
        
        //the lists that will show the items on the page
        public List<Genre> Genres = new List<Genre>();
        public List<GenreStripboek> stripboekgenreshowed = new List<GenreStripboek>();
        public List<Gebruikers_Stripboeken> gebruikerlijstshow = new List<Gebruikers_Stripboeken>();

        
        public List<Stripboek> searchresults = new List<Stripboek>();

        //variabels
        public int userid;
        public string message;
        public string querysearch;


        public void OnGet()
        {
            userbookcheck();
        }

        //search option
        public void OnPostSearch(string search,int searchtype)
        {
            userbookcheck();
            
            //sets string query to %searchvalue% needed for the Like query  
            querysearch = "%" + search + "%";
            message = "je zoekt op : " + search;
            
            List<GenreStripboek> ownedsearch = new List<GenreStripboek>();
            searchresults = new StripboekRepository().GetSearch(querysearch, searchtype).ToList();
            List<Gebruikers_Stripboeken> searchresultsstripboekuser = new List<Gebruikers_Stripboeken>();
            
            foreach (var stripboek in searchresults)
            {
                //checks if user owns stripboek and then sets the searchresult list for the genrestripboek connection
                foreach (var stripboekuser in stripboekgenreshowed)
                {
                    if (stripboek.Stripboek_id == stripboekuser.Stripboek.Stripboek_id)
                    {
                        ownedsearch.Add(stripboekuser);
                    }
                }
                
                //checks if user owns stripboek and then sets the searchresult list for the userstripboek connection
                foreach (var stripboekuser in gebruikerlijstshow)
                {
                    if (stripboek.Stripboek_id == stripboekuser.Stripboek.Stripboek_id)
                    {
                        searchresultsstripboekuser.Add(stripboekuser);
                    }
                }
            }

            //replaces the list to the search results
            gebruikerlijstshow  = searchresultsstripboekuser;
            stripboekgenreshowed = ownedsearch;

            //geeft een message weer als er geen zoek resultaat is
            if(ownedsearch.Count==0)
            {
                message = "geen zoek resultaat voor :" + search;
            }

        }

        //deletes the usersm stripboek
        public void OnPostDelete(int stripboekid)
        {
            userbookcheck();
            if (new Gebruikers_StripboekenRepository().Delete(stripboekid))
            {
                userbookcheck();
            }

        }

        public IActionResult OnPostInfo(int id)
        {
            return RedirectToPage("/BoekInfo", new {stripboek_id = id});
        }
        
        //sets all the list and gets data from database
        public void userbookcheck()
        {
            useridget();
            
            //gets all genres and the books that the current user has 
            GenreStripboeks = new JoinRepository().joingenrestripboek().ToList();
            gebruikerlijst = new JoinRepository().joingebrstripboekstripboeken(userid).ToList();
            gebruikerlijstshow = gebruikerlijst;
            //checks whether the books in the genre are owned 
            foreach (var gebruikstrip in gebruikerlijst)
            {
                foreach (var genrestripboek in GenreStripboeks)
                {
                    if (gebruikstrip.Stripboek.Stripboek_id == genrestripboek.Stripboek.Stripboek_id)
                    {
                        if (!Genres.Any(genr => genr.genre_id ==genrestripboek.genre.genre_id))
                        {
                            Genres.Add(genrestripboek.genre);
                        }
                        
                        stripboekgenreshowed.Add(genrestripboek);
                    }
                }
            }
        }

        //gets the userinfo from claim list
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
