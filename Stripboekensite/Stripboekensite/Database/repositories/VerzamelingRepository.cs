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
    
    //get 'gebruikers_id'
    public Gebruiker Get(int gebruiker_Id)
    {
        string sql = "SELECT * FROM gebruikers WHERE Gebruikers_id = @gebruikerId";
        
        using var connection = GetConnection();
        var gebruiker = connection.QuerySingle<Gebruiker>(sql, new { gebruikerId });
        return gebruiker;
    }
    //get 'stripboek_id'
    
    
    //add 'druk'
    //add 'uitgave'
    //add 'bandlengte'
    //add 'plaats_gekocht'
    //add 'prijs_gekocht'
    //add 'staat'
    //update collection
}