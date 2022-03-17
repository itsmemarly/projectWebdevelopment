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
    
    //geeft een IEnumerable lijst terug met genres.
    public IEnumerable<Genre> Get()
    {
        string sql = "SELECT * FROM genre";

        using var connection = GetConnection();
        var genres  = connection.Query<Genre>(sql);
        return genres;
    }
    
    
    
}