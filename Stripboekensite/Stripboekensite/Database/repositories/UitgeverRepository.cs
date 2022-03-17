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

    //geeft speciefiek uitgever terug op basis id 
    public Uitgever Get(int Uitgeverid)
    {
        string sql = "SELECT * FROM uitgever WHERE uitgever_id = @Uitgeverid";

        using var connection = GetConnection();
        var uitgever = connection.QuerySingle<Uitgever>(sql, new { Uitgeverid });
        return uitgever;
    }

    //geeft een IEnumerable lijst terug met uitgever.
    public IEnumerable<Uitgever> Get()
    {
        string sql = "SELECT * FROM uitgever";

        using var connection = GetConnection();
        var uitgevers  = connection.Query<Uitgever>(sql);
        return uitgevers;
    }
    
    //voegt nieuwe uitgever toe
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
    
    //verwijdert uitgever
    public bool Delete(int uitgeverid)
    {
        string sql = @"DELETE FROM uitgever WHERE  uitgever_id = @uitgeverid";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { uitgeverid });
        return numOfEffectedRows == 1;
    }
    //updates een uitgever zijn Naam
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