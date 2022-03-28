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
            gebruiker = gebruikerRepository.Get(getEmail());
        }
        
        public IActionResult OnPostEdit(string gebruikersNaam)
        {
            gebruiker.Gebruikersnaam = gebruikersNaam;

            GebruikerRepository gebruikerRepository = new GebruikerRepository();
            gebruikerRepository.Update(gebruiker);
            return RedirectToPage("/Profiel");
        }
        
        public string getEmail()
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