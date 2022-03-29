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
    
    //gives back a list of al creators 
    public IEnumerable<Creator> Get()
    {
        string sql = "SELECT * FROM Creator";

        using var connection = GetConnection();
        var Creators  = connection.Query<Creator>(sql);
        return Creators;
    }
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
    
}
