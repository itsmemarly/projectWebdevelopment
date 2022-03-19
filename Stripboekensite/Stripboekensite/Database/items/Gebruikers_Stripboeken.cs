namespace Stripboekensite;

public class Gebruikers_Stripboeken
{
   
        public int Gebruiker_stripboek_ID { get; set; }
    
        public int Gebruiker_id { get; set; }
    
        public int stripboek_id { get; set; }
    
        public int druk { get; set; }
    
        public string uitgave { get; set; }
        
        public float bandlengte { get; set; }
        
        public string plaats_gekocht { get; set; }
        
        public string prijs_gekocht { get; set; }
        
        public string staat { get; set; }
        
        public Stripboek Stripboek { get; set; }
        
        public Gebruiker Gebruiker { get; set; }
        
        
        
    
}