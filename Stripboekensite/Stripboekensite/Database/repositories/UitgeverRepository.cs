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

    //gives back a certain uitgever using their id
    public Uitgever Get(int Uitgeverid)
    {
        string sql = "SELECT * FROM uitgever WHERE uitgever_id = @Uitgeverid";

        using var connection = GetConnection();
        var uitgever = connection.QuerySingle<Uitgever>(sql, new { Uitgeverid });
        return uitgever;
    }
    
    public bool checkid(int Uitgeverid)
    {
        string sql = "SELECT * FROM uitgever WHERE uitgever_id = @Uitgeverid";

        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new { Uitgeverid });
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
    
    //removes uitgever uit database
    public bool Delete(int uitgeverid)
    {
        string sql = @"DELETE FROM uitgever WHERE  uitgever_id = @uitgeverid";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { uitgeverid });
        return numOfEffectedRows == 1;
    }
    //updates an uitgever their name
    public Uitgever Update(Uitgever uitgever)
    {
        string sql = @"
                UPDATE uitgever SET 
                    Naam = @Naam 
                WHERE uitgever_id = @Uitgever_id;
                SELECT * FROM uitgever WHERE uitgever_id = @Uitgever_id";

        using var connection = GetConnection();
        var updateduitgever = connection.QuerySingle<Uitgever>(sql, uitgever);
        return updateduitgever;
    }
    
}