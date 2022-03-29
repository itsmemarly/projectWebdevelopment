using Microsoft.AspNetCore.Mvc.RazorPages;

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
    
     public void OnPostUpdateGebruiker_Stripboek(int gebruiker_stripboek_id,int druk, string uitgave, float bandlengte,string plaats_gekocht, string prijs_gekocht, string staat, List<int> gebruikerids)
        {
            setlists(Gebruiker_stripboek_ID); // calls upon set list to re get all data from database
            gebruikers_stripboeken.stripboek_id = gebruikers_stripboeken.stripboek_id; // gets id from class object
            if (gebruikers_stripboeken.stripboek_id != null)  // gets id from class object if class object exists
            {
                gebruikers_stripboeken.stripboek_id = gebruikers_stripboeken.stripboek_id;
            }
            Gebruikers_Stripboeken addedinfostripboek = new Gebruikers_Stripboeken();
            
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

            addedgebruikers_stripboeken = new Gebruikers_StripboekenRepository().Update(gebruikers_stripboeken);
   
        }





}