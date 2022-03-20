namespace Stripboekensite;

public class CreatorStripboeken
{
    public int Stripboek_id { get; set; }
    public int Creator_id { get; set; }
    public string taak { get; set; }
    
    public Creator Creator { get; set; }
    public Stripboek Stripboek  { get; set; }
}