using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages.Account;

public class AccessDenied : PageModel
{
    //if someone is on this page they tried to access a page they don't have the permission to.
    //For now it should suffice to automatically redirect them to the home page
    public IActionResult OnGet()
    {
        return RedirectToPage("/Index");
    }
}