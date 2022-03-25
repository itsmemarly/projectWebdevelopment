using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;
[Authorize(Roles = Gebruiker.GebruikersRollen.Moderator)]
public class Dashboard : PageModel
{
    public void OnGet()
    {
        
    }
}