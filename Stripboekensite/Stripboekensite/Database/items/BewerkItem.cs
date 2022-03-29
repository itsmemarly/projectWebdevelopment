namespace Stripboekensite;

public class BewerkItem
{
    public List<Genre> Genres { get; set; }

    public List<Uitgever> uitgevers { get; set; }
    public List<Reeks> reeksen { get; set; }
    public List<Creator> creators { get; set; }

    public List<CreatorStripboeken> CreatorStripboeken { get; set; }
    public List<GenreStripboek> genrestripboeks { get; set; }
    
    public Stripboek stripboek { get; set; }
    
}