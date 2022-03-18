using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        
        [BindProperty]
        public Credential credential { get; set; }
        
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            //check if the data is valid
            if (!ModelState.IsValid) return Page();
            
            //make a db connection and get the user from db
            Gebruiker gebruiker;
            {
                GebruikerRepository gebruikersRep = new GebruikerRepository();
                
                //check if the user exists
                if (!gebruikersRep.NameCheck(credential.UserName)) return Page();
                
                gebruiker = gebruikersRep.Get(credential.UserName);
            }
            
           
            //check if the password is correct
            //todo figure out how to properly hash the password
            if (credential.Password.GetHashCode().ToString() == gebruiker.versleuteld_wachtwoord){
                //create a new security context
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, gebruiker.Gebruikersnaam),
                    new Claim(ClaimTypes.Name, gebruiker.naam)
                };
                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
                

                return RedirectToPage("/Index");
            }

            return Page();
        }

        public class Credential
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            public string UserName { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}