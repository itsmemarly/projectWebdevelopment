using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

[Authorize(Roles = "Moderator")]
public class BekijkBoeken : PageModel
{
    public List<Stripboek> Stripboeken = new List<Stripboek>();

    public IActionResult OnGet(string id)
    {
        if (id != null)
        {
            return RedirectToPage("/BoekBewerken", new {stripboek_id = id});
        }
        else
        {
            StripboekRepository stripboekRepository = new StripboekRepository();
            Stripboeken = stripboekRepository.Get().ToList();
            return null;
        }
    }
}