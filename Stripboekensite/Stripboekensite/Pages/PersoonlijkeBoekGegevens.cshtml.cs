using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Stripboekensite.Pages;

public class PersoonlijkeBoekGegevens : PageModel
{
    #region variabels
    public Gebruikers_Stripboeken gebruikers_stripboeken = new Gebruikers_Stripboeken();

    public Stripboek Stripboek = new Stripboek();
    
    public int stripid { get; set; }

    #endregion
    
    // gets id from cookie
    public int Gebruiker_stripboek_ID { get; set;}
    public void OnGet(int id)
    {
        Gebruiker_stripboek_ID = id;
    }
    
     public void OnPostUpdateGebruiker_Stripboek(int Gebruiker_stripboek_ID,int druk, string uitgave, float bandlengte,string plaats_gekocht, string prijs_gekocht, string staat, List<int> gebruikerids)
        {
            setlists(); // sets all variables of gebruikers_stripboeken
            Gebruikers_Stripboeken addedinfostripboek = new Gebruikers_Stripboeken();
            gebruikers_stripboeken.Gebruiker_stripboek_ID = Gebruiker_stripboek_ID;
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

            addedinfostripboek = new Gebruikers_StripboekenRepository().Add(gebruikers_stripboeken);
   
        }
     
     public void setlists()
     {
         //var claims =ClaimsPrincipal.Current.Identities.First().Claims.ToList();
         List<Claim> claims = User.Claims.ToList();

         foreach (var claim in claims)
         {
             if (claim.Type == ClaimTypes.NameIdentifier)
             {

                 Gebruiker_stripboek_ID = int.Parse(claim.Value);
             }
         }
         
     }





}