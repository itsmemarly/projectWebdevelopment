using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages.Account;

public class AccessDenied : PageModel
{
    public IActionResult OnGet()
    {
        return RedirectToPage("/Index");
    }
}