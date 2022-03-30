using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Stripboekensite;

public class Gebruikers_Stripboeken
{
        [BindProperty] public Gebruikers_Stripboeken gebruikersStripboeken { get; set; }
        
        [Required]
        public int Gebruiker_stripboek_ID { get; set; }
        
        [Required]
        public int Gebruiker_id { get; set; }
        
        [Required]
        public int stripboek_id { get; set; }
    
        [DataType(DataType.Custom)]
        public int druk { get; set; }
    
        [DataType(DataType.Custom)]
        public string uitgave { get; set; }
        
        [DataType(DataType.Custom)]
        [Display(Name = "Bandlengte in cm")]
        public float bandlengte { get; set; }
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {100} characters long.", MinimumLength = 2)]
        [DataType(DataType.Text)]
        public string plaats_gekocht { get; set; }
        
        [DataType(DataType.Currency)]
        public string prijs_gekocht { get; set; }
        
        [DataType(DataType.Text)]
        public string staat { get; set; }
        
        public Stripboek Stripboek { get; set; }
        
        public Gebruiker Gebruiker { get; set; }
        
        public List<Genre> Genres { get; set; }
        
        
        
    
}