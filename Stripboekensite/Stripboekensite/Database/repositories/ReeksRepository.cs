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
    

    /* does not get used
         //gives back a specific reeks using its id
    public Reeks Get(int reeksid)
    {
        string sql = "SELECT * FROM reeksen WHERE reeks_id = @reeksid";

        using var connection = GetConnection();
        var reeks = connection.QuerySingle<Reeks>(sql, new { reeksid });
        return reeks;
    }
    
    //gives back whether asked id exists
    public bool checkid(int reeksid)
    {
        string sql = "SELECT * FROM reeksen WHERE reeks_id = @reeksid";

        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new { reeksid });;
    }
     
         //removes reeks from databse using id 
    public bool Delete(int reeksid)
    {
        string sql = @"DELETE FROM reeksen WHERE  reeks_id = @reeksid";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { reeksid });
        return numOfEffectedRows == 1;
    }
    
    //updates a reeks its name and aantal using id 
    public Reeks Update(Reeks reeks)
    {
        string sql = @"
                UPDATE reeksen SET 
                    Reeks_titel = @Reeks_titel and aantal = @Aantal
                WHERE reeks_id = @Reeks_id;
                SELECT * FROM reeksen WHERE reeks_id = @Reeks_id";

        using var connection = GetConnection();
        var updatedreeks = connection.QuerySingle<Reeks>(sql, reeks);
        return updatedreeks;
    }
     */
    
    
}