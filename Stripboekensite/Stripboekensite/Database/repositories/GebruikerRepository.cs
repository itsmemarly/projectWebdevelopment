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
    
    
    //returns a gebruiker using it's username

    public Gebruiker Get(string gebruikersNaam)
    {
        string sql = "SELECT * FROM gebruikers WHERE Gebruikersnaam = @gebruikersNaam";
        
        using var connection =  GetConnection();
        Gebruiker gebruiker = connection.QuerySingle<Gebruiker>(sql, new {gebruikersNaam});
        
        return gebruiker;
    }

    //gives back true if user name exists in db
    public bool NameCheck(string gebruikersNaam)
    {
        string sql = "SELECT COUNT(1) FROM gebruikers WHERE gebruikersnaam = @gebruikersNaam";

        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new {gebruikersNaam});
    }

    //adds new gebruiker
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
    
    //updates a gebruiker their gebruikersnaam(wachtwoord, rol etc. updates can be added)
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
    
    
    
    
    /* does not get used
     
         //gives back a specific gebruiker using its id
    public Gebruiker Get(int gebruikerId)
    {
        string sql = "SELECT * FROM gebruikers WHERE Gebruikers_id = @gebruikerId";

        using var connection = GetConnection();
        var gebruiker = connection.QuerySingle<Gebruiker>(sql, new {gebruikerId}); 
        return gebruiker;
    }
     
         //gives back a specific gebruiker using its name
    public bool IdCheck(int gebruikerId )
    {
        string sql = "SELECT * FROM gebruikers WHERE Gebruikers_id = @gebruikerId";

        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new {gebruikerId}); 
    }
     
        //gives back a list of gebruikers
    public IEnumerable<Gebruiker> Get()
    {
        string sql = "SELECT * FROM gebruikers";

        using var connection = GetConnection();
        var gebruikers  = connection.Query<Gebruiker>(sql);
        return gebruikers;
    } 
     
//deletes gebruiker using id
public bool Delete(int GebruikersId)
{
    string sql = @"DELETE FROM gebruikers WHERE Gebruikers_id = @GebruikersId";

    using var connection = GetConnection();
    int numOfEffectedRows = connection.Execute(sql, new { GebruikersId });
    return numOfEffectedRows == 1;
}
      */
}