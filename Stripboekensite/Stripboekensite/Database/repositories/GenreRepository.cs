using System.Collections.Generic;
using System.Data;
using Dapper;


namespace Stripboekensite;

public class GenreRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    public IEnumerable<genre> Get()
    {
        string sql = "SELECT * FROM genre";

        using var connection = GetConnection();
        var genres  = connection.Query<genre>(sql);
        return genres;
    }
    
    
    
}