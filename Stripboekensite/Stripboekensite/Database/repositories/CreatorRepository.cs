using System.Collections.Generic;
using System.Data;
using Dapper;


namespace Stripboekensite;

public class CreatorRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    
    //geeft speciefiek creator terug op basis id 
    public Creator Get(int Creator_ID)
    {
        string sql = "SELECT * FROM Creator WHERE creator_ID = @Creator_ID";

        using var connection = GetConnection();
        var creator = connection.QuerySingle<Creator>(sql, new { Creator_ID });
        return creator;
    }

    //geeft een IEnumerable lijst terug met creator.
    public IEnumerable<Creator> Get()
    {
        string sql = "SELECT * FROM Creator";

        using var connection = GetConnection();
        var Creators  = connection.Query<Creator>(sql);
        return Creators;
    }
    
    //voegt nieuwe creator toe
    public Creator Add(Creator creator)
    {
        string sql = @"
                INSERT INTO Creator (creator_naam) 
                VALUES (@Creator_naam); 
                SELECT * FROM Creator WHERE Creator_id = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwCreator = connection.QuerySingle<Creator>(sql, creator);
        return nieuwCreator;
    }
    
    //verwijdert creator
    public bool Delete(int creatorid)
    {
        string sql = @"DELETE FROM creator WHERE  creator_ID = @creatorid";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { creatorid });
        return numOfEffectedRows == 1;
    }
    //updates een creator zijn Naam
    public Creator Update(Creator creator)
    {
        string sql = @"
                UPDATE creator SET 
                    creator_naam = @Creator_naam 
                WHERE creator_ID = @Creator_id;
                SELECT * FROM creator WHERE creator_ID = @Creator_id";

        using var connection = GetConnection();
        var updatedCreator = connection.QuerySingle<Creator>(sql, creator);
        return updatedCreator;
    }

    
}