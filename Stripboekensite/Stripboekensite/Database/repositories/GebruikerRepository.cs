using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class GebruikerRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    //geeft specifiek gebruiker terug op basis id 
    public Gebruiker Get(int gebruikerId)
    {
        string sql = "SELECT * FROM gebruikers WHERE Gebruikers_id = @gebruikerId";

        using var connection = GetConnection();
        var gebruiker = connection.QuerySingle<Gebruiker>(sql, new { gebruikerId });
        return gebruiker;
    }

    //geeft een IEnumerable lijst terug met gebruikers.
    public IEnumerable<Gebruiker> Get()
    {
        string sql = "SELECT * FROM gebruikers";

        using var connection = GetConnection();
        var gebruikers  = connection.Query<Gebruiker>(sql);
        return gebruikers;
    }
    
    //voegt nieuwe gebruiker toe
    public Gebruiker Add(Gebruiker gebruiker)
    {
        string sql = @"
                INSERT INTO gebruikers (Gebruikersnaam, versleuteld_wachtwoord,rol,naam) 
                VALUES (@Gebruikersnaam,@versleuteld_wachtwoord, @rol, @naam); 
                SELECT * FROM gebruikers WHERE Gebruikers_id = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwgebruiker = connection.QuerySingle<Gebruiker>(sql, gebruiker);
        return nieuwgebruiker;
    }
    
    //verwijdert gebruiker
    public bool Delete(int GebruikersId)
    {
        string sql = @"DELETE FROM gebruikers WHERE Gebruikers_id = @GebruikersId";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { GebruikersId });
        return numOfEffectedRows == 1;
    }
    //updates een gebruiker zijn gebruikersnaam(wachtwoord rol enzo updates kunnen worden toegevoegd)
    public Gebruiker Update(Gebruiker gebruiker)
    {
        string sql = @"
                UPDATE gebruikers SET 
                    gebruikersnaam = @gebruikersnaam 
                WHERE Gebruikers_id = @gebruikers_id;
                SELECT * FROM gebruikers WHERE Gebruikers_id = @gebruikers_id";

        using var connection = GetConnection();
        var updatedgebruiker = connection.QuerySingle<Gebruiker>(sql, gebruiker);
        return updatedgebruiker;
    }
}