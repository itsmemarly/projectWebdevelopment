using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class UitgeverRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }

    //gives back the list of uitgevers
    public IEnumerable<Uitgever> Get()
    {
        string sql = "SELECT * FROM uitgever";

        using var connection = GetConnection();
        var uitgevers  = connection.Query<Uitgever>(sql);
        return uitgevers;
    }
    
    //adds a uitgever to the database
    public Uitgever Add(Uitgever uitgever)
    {
        string sql = @"
                INSERT INTO uitgever (naam) 
                VALUES (@Naam); 
                SELECT * FROM uitgever WHERE uitgever_id = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwUitgever = connection.QuerySingle<Uitgever>(sql, uitgever);
        return nieuwUitgever;
    }
    
    
}