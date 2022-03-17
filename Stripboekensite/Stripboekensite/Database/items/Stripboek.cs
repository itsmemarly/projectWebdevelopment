namespace Stripboekensite;

public class Stripboek
{
        public int Stripboek_id { get; set; }
        
        public string isbn { get; set; }
        public string titel { get; set; }
        
        public int Uitgave1e_druk { get; set; }
        public int Reeks_nr { get; set; }
        public int Bladzijden { get; set; }
        
        public int expleciet { get; set; }
        
        public int Reeks_id { get; set; }
        public int Uitgever_id { get; set; }
        
        public Reeks reeks { get; set; }
        public Uitgever uitgever { get; set; }
}