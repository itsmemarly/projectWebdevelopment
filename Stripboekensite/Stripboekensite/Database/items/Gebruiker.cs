namespace Stripboekensite;

public class Gebruiker
{
    public int Gebruikers_id { get; set; }
    
    public string Gebruikersnaam { get; set; }
    
    public DateTime Geboorte_datum { get; set; }
    
    public string versleuteld_wachtwoord { get; set; }
    
    public string rol { get; set; }
    
    public string naam { get; set; }

    public static class GebruikersRollen
    {
        public const string Gebruiker = "Gebruiker";
        public const string Moderator = "Moderator";
    }
}