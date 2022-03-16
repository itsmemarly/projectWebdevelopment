using System.ComponentModel.DataAnnotations;

namespace Stripboekensite;

public class genre
{
    public int GenreId { get; set; }

    //[Required, MinLength(2), MaxLength(128)]
    public string Soort { get; set; }
}