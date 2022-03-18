using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class Gebruikers_StripboekenRepository
{
    //connect to database
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }

    //add information of books to own collection
    
    //gives back specific gebruiker_stripboek stripboek combination
    public Gebruikers_Stripboeken Get(int Gebruiker_stripboek_ID, int Stripboek_ID)
    {
        string sql = "SELECT * FROM gebruikers_stripboeken WHERE Gebruiker_stripboek_ID = @Gebruiker_stripboek_ID and stripboek_id = @Stripboek_ID";

        using var connection = GetConnection();
        var gebrStripboeken = connection.QuerySingle<Gebruikers_Stripboeken>(sql, new { Gebruiker_stripboek_ID, Stripboek_ID});
        return gebrStripboeken;
    }

    //get 'gebruikers_id & stripboek_id'
    public Gebruikers_Stripboeken Get(int gebruiker, int stripboek)
    {
        string sql = "SELECT * FROM gebruikers WHERE Gebruikers_ID = @gebruiker AND WHERE stripboek_id = @stripboek";

        using var connection = GetConnection();
        var gebruikerenstripboekopgehaald = connection.QuerySingle<Gebruikers_Stripboeken>(sql, new { gebruiker, stripboek });
        return gebruikerenstripboekopgehaald;
    }
    
    //get an ienumerable list of 'stripboeken'
    public IEnumerable<Stripboek> Get()
    {
        string sql = "SELECT * FROM stripboeken";

        using var connection = GetConnection();
        var stripboeken  = connection.Query<Stripboek>(sql);
        return stripboeken;
    }

    //add druk, bandlengte, plaats_gekocht, prijs_gekocht en staat
    public Gebruikers_Stripboeken Add(Gebruikers_Stripboeken gebruikers_stripboeken)
    {
        string sql = @"
                INSERT INTO gebruikers_stripboeken (druk, uitgave, bandlengte, plaats_gekocht, prijs_gekocht, staat)
                VALUES (@druk, @uitgave, @bandlengte, @plaats_gekocht, @prijs_gekocht, @staat); 
                SELECT * FROM gebruikers_stripboeken WHERE stripboek_id = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwstripboekverzameling = connection.QuerySingle<Gebruikers_Stripboeken>(sql, gebruikers_stripboeken);
        return nieuwstripboekverzameling;
    }
    
    //update join gebruikers_stripboeken and stripboeken
    
    
    

    //update collection
    public Gebruikers_Stripboeken Update(Gebruikers_Stripboeken gebruikers_stripboeken)
    {
        string sql = @"
                UPDATE gebruikers_stripboeken SET
                Gebruikers_stripboek_ID = Gebruikers_stripboek_ID
                WHERE Gebruikers_stripboek_ID = @Gebruikers_stripboek_ID;
                SELECT * FROM gebruikers_stripboeken WHERE Gebruikers_stripboek_ID = @Gebruikers_stripboek_ID";

        using var connection = GetConnection();
        var updatedverzameling = connection.QuerySingle<Gebruikers_Stripboeken>(sql, gebruikers_stripboeken);
        return updatedverzameling;
    }
}
    