namespace Stripboekensite;

public class Gebruiker
{
    public int Gebruikers_id { get; set; }
    
    public string Gebruikersnaam { get; set; }
    
    public string versleuteld_wachtwoord { get; set; }
    
    public string rol { get; set; }
    
    public string naam { get; set; }
}