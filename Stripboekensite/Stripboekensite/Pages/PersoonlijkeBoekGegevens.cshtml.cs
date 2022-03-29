using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Stripboekensite.Pages;

public class PersoonlijkeBoekGegevens : PageModel
{
    #region variabels
    public Gebruikers_Stripboeken gebruikers_stripboeken = new Gebruikers_Stripboeken();
    public int stripboek_id { get; set;}
    #endregion
    
    
    public void OnGet(int id)
    {
        stripboek_id = id;
    }
    
     public IActionResult OnPostSend(int stripboek_id, int druk, string uitgave, float bandlengte,string plaats_gekocht, string prijs_gekocht, string staat)
        {
            GetUserID(); // sets all variables of gebruikers_stripboeken
            gebruikers_stripboeken.Gebruiker_id = GetUserID(); // gets id from cookie
            gebruikers_stripboeken.stripboek_id = stripboek_id;
            gebruikers_stripboeken.druk = druk;
            gebruikers_stripboeken.uitgave = uitgave;
            gebruikers_stripboeken.bandlengte = bandlengte;
            gebruikers_stripboeken.plaats_gekocht = plaats_gekocht;
            gebruikers_stripboeken.prijs_gekocht = prijs_gekocht;
            gebruikers_stripboeken.staat = staat;
            
            //checks if anything has been filled and if nothing or 0 has been given as a value, nothing will change
            if (druk != 0)
            {
                gebruikers_stripboeken.druk = druk;
            }
            if (uitgave != null)
            {
                gebruikers_stripboeken.uitgave = uitgave;
            }
            if (bandlengte != 0)
            {
                gebruikers_stripboeken.bandlengte = bandlengte;
            } 
            if (plaats_gekocht != null)
            {
                gebruikers_stripboeken.plaats_gekocht = plaats_gekocht;
            }
            if (prijs_gekocht != null)
            {
                gebruikers_stripboeken.prijs_gekocht = prijs_gekocht;
            }
            
            if (staat != null)
            {
                gebruikers_stripboeken.staat = staat;
            }
            
            //adds gebruiker_stripboek to the repostitory 
            new Gebruikers_StripboekenRepository().Add(gebruikers_stripboeken);

            return RedirectToPage("/Boekenkast");
        }
     
     public int GetUserID()
     {
         //var claims =ClaimsPrincipal.Current.Identities.First().Claims.ToList();
         List<Claim> claims = User.Claims.ToList();

         foreach (var claim in claims)
         {
             if (claim.Type == ClaimTypes.NameIdentifier)
             {

                 return int.Parse(claim.Value);
             }
         }
        
         //return an incorrect user id, but this code should never be reached
         //because we can only access this page if the user is authorized
         return -1;

     }





}