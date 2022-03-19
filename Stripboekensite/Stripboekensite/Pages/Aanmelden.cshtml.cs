using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Stripboekensite.Pages
{
    public class AanmeldenModel : PageModel
    {
        [BindProperty] public GebruikerData gebruikerData { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            {
                GebruikerRepository gebruikerRepository = new GebruikerRepository();
                if (gebruikerRepository.NameCheck(gebruikerData.gebruikersnaam)) return Page();

                Gebruiker gebruiker = new Gebruiker();
                gebruiker.naam = gebruikerData.naam;
                gebruiker.Gebruikersnaam = gebruikerData.gebruikersnaam;
                gebruiker.versleuteld_wachtwoord =
                    new PasswordHasher<object?>().HashPassword(null, gebruikerData.password);

                gebruiker.rol = "Gebruiker";

                gebruikerRepository.Add(gebruiker);
                return RedirectToPage("/inloggen");
            }

            return Page();
        }

        public class GebruikerData
        {
            [Required]
            [Display(Name = "Name")] 
            [StringLength(32, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            public string naam { get; set; }
            
            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Email")] 
            public string gebruikersnaam { get; set; }
            
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Password")]  
            [DataType(DataType.Password)]
            public string password { get; set; }
            
            [Required]
            [Display(Name = "Confirm password")]
            [Compare(nameof(password), ErrorMessage = "The password and confirmation password do not match.")]
            public string confirmationPassword { get; set; }
        }
    }
}
