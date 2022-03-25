using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Crypto.Parameters;

namespace Stripboekensite.Pages;

[Authorize(Roles = "Moderator")]
public class BekijkGebruikers : PageModel
{
    public List<Gebruiker>Gebruikers = new List<Gebruiker>();
    public void OnGet()
    {
        GebruikerRepository gebruikerRepository = new GebruikerRepository();
        Gebruikers = gebruikerRepository.Get().ToList();
    }
}