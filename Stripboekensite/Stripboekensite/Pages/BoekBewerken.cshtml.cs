using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

public class BoekBewerken : PageModel
{
    public void OnGet(string stripboek_id)
    {
        Console.WriteLine(stripboek_id);
    }
}