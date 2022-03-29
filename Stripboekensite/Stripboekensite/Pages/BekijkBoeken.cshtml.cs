using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

[Authorize(Roles = Gebruiker.GebruikersRollen.Moderator)]
public class BekijkBoeken : PageModel
{
    public List<Stripboek> Stripboeken = new List<Stripboek>();

    //when the page loads, get all the books so we can display them
    public void OnGet(string id)
    {
        StripboekRepository stripboekRepository = new StripboekRepository();
        Stripboeken = stripboekRepository.Get().ToList();
    }

    //if the edit button is clicked, redirect them to the edit page with the id of the book the admin wishes to edit
    public IActionResult OnPostEdit(string id)
    {
        return RedirectToPage("/BoekBewerken", new {stripboek_id = id});
    }

    public IActionResult OnPostDelete(string id)
    {
        int stripboek_id = Int32.Parse(id);
        
        //first delete all references to the book in the gebruikers_stripboeken table
        var gebruikersStripboekenRepository = new Gebruikers_StripboekenRepository();
        gebruikersStripboekenRepository.DeleteStripboek(stripboek_id);
    
    
        //then delete all references to the book in teh genre_stripboeken table
        var genreStripboekRepository = new GenreStripboekRepository();
        genreStripboekRepository.DeleteByBookID(stripboek_id);

        //then delete all references to the book in creators_stripboeken table
        CreatorStripboekenRepository creatorStripboekenRepository = new CreatorStripboekenRepository();
        creatorStripboekenRepository.DeleteByStripboek(stripboek_id);
    
    
        //finally delete the book itself
        StripboekRepository stripboekRepository = new StripboekRepository();
        stripboekRepository.Delete(stripboek_id);
        
        return RedirectToPage("/BekijkBoeken");
    }
}