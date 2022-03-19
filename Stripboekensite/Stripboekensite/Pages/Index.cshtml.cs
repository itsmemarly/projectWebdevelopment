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
        
        public string message;
        public string querysearch;
        //int userid through cookies

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            userbookcheck(6);

        }

        public void OnPostSearch(string search)
        {
            userbookcheck(6);
            
            querysearch = "%" + search + "%";
            //search gedoe
            message = search;
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

        }

        public void userbookcheck(int userid)
        {
            Genres = new GenreRepository().Get().ToList();
            GenreStripboeks = new JoinRepository().joingenrestripboek().ToList();
            //de 6 moet straks nog op userid gezet worden
            gebruikerlijst = new JoinRepository().joingebrstripboekstripboeken(userid).ToList();
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