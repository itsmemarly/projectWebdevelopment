using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty] public Credential credential { get; set; } = new Credential();
        
        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            //check if the data is valid
            if (!ModelState.IsValid)
            {
                
                return null;
            }
            
            //make a db connection and get the user from db
            Gebruiker gebruiker;
            {
                GebruikerRepository gebruikersRep = new GebruikerRepository();
                
                //check if the user exists
                if (!gebruikersRep.NameCheck(credential.UserName))
                {
                    ModelState.AddModelError("LogOnError", "The user name or password provided is incorrect.");
                    return null;
                }
                
                gebruiker = gebruikersRep.Get(credential.UserName);
            }
            
           
            //check if the password is correct
            switch (new PasswordHasher<object?>().VerifyHashedPassword(null, gebruiker.versleuteld_wachtwoord, credential.Password) ){
                //if it's correct sign the user in
                case PasswordVerificationResult.Success:
                    SignInUser(gebruiker);
                    RedirectToPage("/Index");
                    return null;
                //if it isn't keep the user on the page
                case PasswordVerificationResult.Failed:
                    ModelState.AddModelError("LogOnError", "The user name or password provided is incorrect.");
                    return null;
                //if it's correct but the password needs to be rehashed, rehash the password and sign the user in
                case PasswordVerificationResult.SuccessRehashNeeded:
                    SignInUser(gebruiker);
                    gebruiker.versleuteld_wachtwoord =
                        new PasswordHasher<object?>().HashPassword(null, credential.Password);
                    {
                        GebruikerRepository gebruikerRepository = new GebruikerRepository();
                        gebruikerRepository.Update(gebruiker);
                    }
                    
                    return RedirectToPage("/Index");
            }

            return null;
        }

        private async void SignInUser(Gebruiker gebruiker)
        {
            
            //create a new security context
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, gebruiker.Gebruikersnaam),
                new Claim(ClaimTypes.Name, gebruiker.naam),
                new Claim(ClaimTypes.Role, gebruiker.rol),
                new Claim(ClaimTypes.NameIdentifier , gebruiker.Gebruikers_id.ToString())
            };
            //create a new identity with the appropriate claims
            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            
            //create a claimsPrincipal and associate it with the identity
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
            //add it to the http context ass a cookie
            await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
        }
        
        public class Credential
        {
            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email Address")]
            
            public string UserName { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Password")]  
            public string Password { get; set; }
        }
    }
}
