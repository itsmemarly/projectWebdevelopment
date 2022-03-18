using System.Collections.Generic;
using System.Data;
using Dapper; 

namespace Stripboekensite;

public class VerzamelingRepository
{
    //connect to database
    private IDbConnection getConnection()
    {
        return new DbUtils().GetDbConnection();
    }

    //add information of books to own collection

    //get 'gebruikers_id & stripboek_id'
    public Verzameling Get(int gebruiker, int stripboek)
    {
        string sql = "SELECT * FROM gebruikers WHERE Gebruikers_ID = @gebruiker AND WHERE stripboek_id = @stripboek";

        using var connection = GetConnection();
        var gebruikerenstripboekopgehaald = connection.QuerySingle<Verzameling>(sql, new { gebruiker, stripboek });
        return gebruikerenstripboekopgehaald;
    }

    //add druk, bandlengte, plaats_gekocht, prijs_gekocht en staat
    public Verzameling Add(Verzameling gebruikers_stripboeken)
    {
        string sql = @"
                INSERT INTO gebruikers_stripboeken (druk, uitgave, bandlengte, plaats_gekocht, prijs_gekocht, staat)
                VALUES (@druk, @uitgave, @bandlengte, @plaats_gekocht, @prijs_gekocht, @staat); 
                SELECT * FROM gebruikers_stripboeken WHERE stripboek_id = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwstripboekverzameling = connection.QuerySingle<Verzameling>(sql, gebruikers_stripboeken);
        return nieuwstripboekverzameling;
    }

    //update collection
    public Verzameling Update(Verzameling gebruikers_stripboeken)
    {
        string sql = @"
                UPDATE gebruikers_stripboeken SET
                Gebruikers_stripboek_ID = Gebruikers_stripboek_ID
                WHERE Gebruikers_stripboek_ID = @Gebruikers_stripboek_ID;
                SELECT * FROM gebruikers_stripboeken WHERE Gebruikers_stripboek_ID = @Gebruikers_stripboek_ID";

        using var connection = GetConnection();
        var updatedverzameling = connection.QuerySingle<Verzameling>(sql, gebruikers_stripboeken);
        return updatedverzameling;
    }
}
    