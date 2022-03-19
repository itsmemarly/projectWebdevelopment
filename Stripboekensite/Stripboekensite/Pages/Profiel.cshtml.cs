using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages
{
    [Authorize]
    public class ProfielModel : PageModel
    {
        
        public void OnGet()
        {
        }
    }
}
