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
        
        
        
    //geeft speciefiek reeks terug op basis id 
    public Reeks Get(int reeksid)
    {
        string sql = "SELECT * FROM reeksen WHERE reeks_id = @reeksid";

        using var connection = GetConnection();
        var reeks = connection.QuerySingle<Reeks>(sql, new { reeksid });
        return reeks;
    }

    //geeft een IEnumerable lijst terug met reeksen.
    public IEnumerable<Reeks> Get()
    {
        string sql = "SELECT * FROM reeksen";

        using var connection = GetConnection();
        var reeksen  = connection.Query<Reeks>(sql);
        return reeksen;
    }
    
    //voegt nieuwe reeks toe
    public Reeks Add(Reeks reeks)
    {
        string sql = @"
                INSERT INTO reeksen (Reeks_titel,aantal) 
                VALUES (@Reeks_titel, @Aantal); 
                SELECT * FROM uitgever WHERE uitgever_id = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwReeks = connection.QuerySingle<Reeks>(sql, reeks);
        return nieuwReeks;
    }
    
    //verwijdert reeks
    public bool Delete(int reeksid)
    {
        string sql = @"DELETE FROM reeksen WHERE  reeks_id = @reeksid";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { reeksid });
        return numOfEffectedRows == 1;
    }
    //updates een reeks zijn Naam
    public Reeks Update(Reeks reeks)
    {
        string sql = @"
                UPDATE reeksen SET 
                    Reeks_titel = @Reeks_titel 
                WHERE reeks_id = @Reeks_id;
                SELECT * FROM reeksen WHERE reeks_id = @Reeks_id";

        using var connection = GetConnection();
        var updatedreeks = connection.QuerySingle<Reeks>(sql, reeks);
        return updatedreeks;
    }
    
        
        
}