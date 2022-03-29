using System.Collections.Generic;
using System.Data;
using Dapper;


namespace Stripboekensite;

public class ReeksRepository
{
    private IDbConnection GetConnection()
        {
            return new DbUtils().GetDbConnection();
        }
    
    //gives back a list of reeksen
    public IEnumerable<Reeks> Get()
    {
        string sql = "SELECT * FROM reeksen";

        using var connection = GetConnection();
        var reeksen  = connection.Query<Reeks>(sql);
        return reeksen;
    }
    
    //adds a reeks to database
    public Reeks Add(Reeks reeks)
    {
        string sql = @"
                INSERT INTO reeksen (Reeks_titel,aantal) 
                VALUES (@Reeks_titel, @Aantal); 
                SELECT * FROM reeksen WHERE reeks_id = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwReeks = connection.QuerySingle<Reeks>(sql, reeks);
        return nieuwReeks;
    }
    
    
}