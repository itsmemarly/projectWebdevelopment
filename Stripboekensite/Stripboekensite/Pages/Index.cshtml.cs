using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Gebruikers_Stripboeken> gebruikerlijst = new List<Gebruikers_Stripboeken>();
        public List<Genre> Genres = new List<Genre>();
        public List<GenreStripboek> GenreStripboeks = new List<GenreStripboek>();
        public List<GenreStripboek> stripboekgenreshowed = new List<GenreStripboek>();
        public List<GenreStripboek> searchresultss = new List<GenreStripboek>();
        
        public List<Stripboek> searchresults = new List<Stripboek>();
        public List<GenreStripboek> ownedsearch = new List<GenreStripboek>();
        
        public int userid;
        
        public string message;
        public string querysearch;
        //int userid through cookies

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var claims =ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            
            userbookcheck(userid);
        }

        public void OnPostSearch(string search)
        {
            userbookcheck(6);
            
            //sets string query to %searchvalue% needed for the Like query  
            querysearch = "%" + search + "%";
           
            message = "je zoekt op :" + search;
            
            //checks if there even is a value in the database if false message will say no search results
            //if true added stripboek showed so the stripboek wil be showed on page
            if (new StripboekRepository().checkSearch(querysearch))
            {
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
            }
            else
            {
                message = "geen zoek resultaat voor:" + search;
            }

        }

        public void userbookcheck(int userid)
        {
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
    }
}