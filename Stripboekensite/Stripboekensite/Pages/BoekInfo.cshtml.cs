using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages;

public class BoekInfo : PageModel
{
    public Stripboek stripboek { get; set; }
    public List<Creator> creators { get; set; }

    public void OnGet(string stripboek_id)
    {
        JoinRepository joinRepository = new JoinRepository();
        stripboek = joinRepository.joinStripboek(Int32.Parse(stripboek_id));
    }
}