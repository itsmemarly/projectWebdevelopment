using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector.Logging;

namespace Stripboekensite.Pages
{
    [Authorize]
    public class ProfielModel : PageModel
    {
        public Gebruiker gebruiker = new Gebruiker();
        public void OnGet()
        {
            GebruikerRepository gebruikerRepository = new GebruikerRepository();
            gebruiker = gebruikerRepository.Get(GetEmail());
        }
        
        public IActionResult OnPostEdit(string naam, DateTime geboorteDatum)
        {
            GebruikerRepository gebruikerRepository = new GebruikerRepository();
            gebruiker = gebruikerRepository.Get(GetEmail());
            
            gebruiker.naam = naam;
            gebruiker.Geboorte_datum = geboorteDatum;
            
            gebruikerRepository.UpdateUserProfile(gebruiker);
            return RedirectToPage("/Profiel");
        }
        
        private string GetEmail()
        {
            List<Claim> claims = User.Claims.ToList();

            foreach (var claim in claims)
            {
                if (claim.Type == ClaimTypes.Email)
                {
                    return claim.Value;
                }
            }

            return null;
        }
    }
}