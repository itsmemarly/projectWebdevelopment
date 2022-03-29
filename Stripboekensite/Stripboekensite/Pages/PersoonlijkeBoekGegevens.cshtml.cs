using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

public class PersoonlijkeBoekGegevens : PageModel
{
    public int stripboek_id { get; set;}
    public void OnGet(int id)
    {
        stripboek_id = id;
    }
}