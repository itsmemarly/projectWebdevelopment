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

    public Gebruikers_Stripboeken Get(int Gebruiker_ID, int stripboek_id)
    {
        string sql = "SELECT * FROM gebruikers_stripboeken WHERE Gebruikers_ID = @Gebruiker_ID and stripboek_id = @stripboek_id";

        using var connection = GetConnection();
        var gebrStripboek = connection.QuerySingle<Gebruikers_Stripboeken>(sql, new { Gebruiker_ID, stripboek_id});
        return gebrStripboek;
    }

    //add druk, bandlengte, plaats_gekocht, prijs_gekocht and staat stripboek and user
    public Gebruikers_Stripboeken Add(Gebruikers_Stripboeken gebruikers_stripboeken)
    {
        string sql = @"
                INSERT INTO gebruikers_stripboeken (druk, uitgave, bandlengte, plaats_gekocht, prijs_gekocht, staat, Gebruikers_ID, stripboek_id)
                VALUES (@druk, @uitgave, @bandlengte, @plaats_gekocht, @prijs_gekocht, @staat, @Gebruiker_id, @stripboek_id); 
                SELECT * FROM gebruikers_stripboeken WHERE Gebruiker_stripboek_ID = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwstripboekverzameling = connection.QuerySingle<Gebruikers_Stripboeken>(sql, gebruikers_stripboeken);
        return nieuwstripboekverzameling;
    }
    
    public bool Delete(int gebruikerstripboekid)
    {
        string sql = @"DELETE FROM gebruikers_stripboeken WHERE Gebruiker_stripboek_ID = @gebruikerstripboekid";
        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new {gebruikerstripboekid});
    }
    
    public bool DeleteGebruiker(int gebruikers_id)
    {
        string sql = @"DELETE FROM gebruikers_stripboeken WHERE Gebruikers_ID = @gebruikers_id";
        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new {gebruikers_id});
    }
    
    public bool DeleteStripboek(int stripboek_id)
    {
        string sql = @"DELETE FROM gebruikers_stripboeken WHERE stripboek_id = @stripboek_id";
        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new {stripboek_id});
    }
    
    
    /* does not get used
      //gives back specific gebruiker_stripboek stripboek combination
    public Gebruikers_Stripboeken Get(int Gebruiker_stripboek_ID)
    {
        string sql = "SELECT * FROM gebruikers_stripboeken WHERE Gebruiker_stripboek_ID = @Gebruiker_stripboek_ID";

        using var connection = GetConnection();
        var gebrStripboeken = connection.QuerySingle<Gebruikers_Stripboeken>(sql, new { Gebruiker_stripboek_ID});
        return gebrStripboeken;
    }
     
     
         //get an ienumerable list of all the 'stripboeken' owned by users
    public IEnumerable<Stripboek> Get()
    {
        string sql = "SELECT * FROM stripboeken";

        using var connection = GetConnection();
        var stripboeken  = connection.Query<Stripboek>(sql);
        return stripboeken;
    }
     
      //get an ienumerable list of 'stripboeken' from a specifiek user
    public IEnumerable<Stripboek> Getfromuser(int Gebruiker_ID)
    {
        string sql = "SELECT * FROM stripboeken WHERE Gebruiker_ID = @Gebruiker_ID";

        using var connection = GetConnection();
        var stripboeken  = connection.Query<Stripboek>(sql, new {Gebruiker_ID});
        return stripboeken;
    }
     
     
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
     */
}
    