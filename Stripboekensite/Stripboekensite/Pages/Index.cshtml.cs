using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
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

        public void OnPost()
        {
            //check if the data is valid
            if (!ModelState.IsValid) return;
            
            //make a db connection and get the user from db
            Gebruiker gebruiker;
            {
                GebruikerRepository gebruikersRep = new GebruikerRepository();
                gebruiker = gebruikersRep.Get(credential.UserName);
            }
            
            //TODO check if gebruiker exists
            
            if (credential.Password.GetHashCode().ToString() == gebruiker.versleuteld_wachtwoord){
                
            }
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