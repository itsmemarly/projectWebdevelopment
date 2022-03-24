using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

[Authorize(Roles = "Moderator")]
public class BekijkBoeken : PageModel
{
    public void OnGet()
    {
        
    }
}