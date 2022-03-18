namespace Stripboekensite;

public class Join
{
    public class Stripboek
    {
        public string isbn { get; set; }
        public string titel { get; set; }
        public int Uitgave1e_druk { get; set; }
        public int Reeks_nr { get; set; }
        public int Bladzijden { get; set; }
        public int expleciet { get; set; }
        public int Reeks_id { get; set; }
        public int Uitgever_id { get; set; }
        public List<Gebruikers_Stripboeken> Gebruikers_Stripboeken { get; set; }
    }
    public class Gebruikers_stripboek
    {
        public int Gebruiker_stripboek_ID { get; set; }
        public int druk { get; set; }
        public string uitgave { get; set; }
        public float bandlengte { get; set; }
        public string plaats_gekocht { get; set; }
        public string prijs_gekocht { get; set; }
        public string staat { get; set; }
        public List<Stripboek> Stripboek { get; set; } 
    }
}