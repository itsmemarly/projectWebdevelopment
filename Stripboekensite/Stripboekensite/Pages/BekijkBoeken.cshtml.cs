using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

[Authorize(Roles = "Moderator")]
public class BekijkBoeken : PageModel
{
    public List<Stripboek> Stripboeken = new List<Stripboek>();
    public void OnGet()
    {
        StripboekRepository stripboekRepository = new StripboekRepository();
        Stripboeken = stripboekRepository.Get().ToList();
    }
}