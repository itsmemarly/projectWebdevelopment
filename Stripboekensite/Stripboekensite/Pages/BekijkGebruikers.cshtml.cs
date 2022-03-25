using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Crypto.Parameters;

namespace Stripboekensite.Pages;

[Authorize(Roles = Gebruiker.GebruikersRollen.Moderator)]
public class BekijkGebruikers : PageModel
{
    public List<Gebruiker>Gebruikers = new List<Gebruiker>();
    public List<SelectListItem> rolOpties = new List<SelectListItem>();
    
    public void OnGet()
    {
        rolOpties.Add( new SelectListItem {Value = Gebruiker.GebruikersRollen.Gebruiker, Text = Gebruiker.GebruikersRollen.Gebruiker});
        rolOpties.Add( new SelectListItem {Value = Gebruiker.GebruikersRollen.Moderator, Text = Gebruiker.GebruikersRollen.Moderator});
        
        
        GebruikerRepository gebruikerRepository = new GebruikerRepository();
        Gebruikers = gebruikerRepository.Get().ToList();
    }

    public IActionResult OnPost(string gebruiker_id, string gebruikersNaam, string geboorte_datum, string email, string rol)
    {
        Gebruiker gebruiker = new Gebruiker();
        gebruiker.naam = gebruikersNaam;
        gebruiker.Gebruikersnaam = email;
        gebruiker.Geboorte_datum = DateTime.Parse(geboorte_datum);
        gebruiker.rol = rol;
        gebruiker.Gebruikers_id = Int32.Parse(gebruiker_id);

        GebruikerRepository gebruikerRepository = new GebruikerRepository();
        gebruikerRepository.Update(gebruiker);
        return RedirectToPage("/BekijkGebruikers");
    }
}