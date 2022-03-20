namespace Stripboekensite;

public class GenreStripboek
{
    public int Stripboek_id { get; set; }
    public int Genre_id { get; set; }
    
    public Stripboek Stripboek  { get; set; }
    public Genre genre { get; set; }
}