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
    
    //gives back a list of al genres
    public IEnumerable<Genre> Get()
    {
        string sql = "SELECT * FROM genre";

        using var connection = GetConnection();
        var genres  = connection.Query<Genre>(sql);
        return genres;
    }
    
    //updates a genre its name/soort
    public Genre Update(Genre genre)
    {
        string sql = @"
                UPDATE genre SET 
                    soort = @soort 
                WHERE genre_id = @genre_id;
                SELECT * FROM genre WHERE genre_id = @genre_id";
        
        using var connection = GetConnection();
        var updatedgenre = connection.QuerySingle<Genre>(sql, genre);
        return updatedgenre;
        
    }
    
    //deletes a genre using its id
    public bool DeleteGenre(int genre_id)
    {
        string sql = @"DELETE FROM genre WHERE genre_id = @genre_id";
        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new {genre_id});
    }
    
    //adds a new genre
    public Genre Add(Genre genre)
    {
        string sql = "INSERT INTO genre (soort) VALUES (@Soort); SELECT * FROM genre WHERE genre_id = LAST_INSERT_ID()";
        using var connection = GetConnection();
        var newgenre = connection.QuerySingle<Genre>(sql, genre);
        return newgenre;
    } 


    /*
         
    //gives back a specific genre its name
    public Genre Get(int genreid)
    {
        string sql = "SELECT * FROM genre where genre_id = @genreid";

        using var connection = GetConnection();
        var genre  = connection.QuerySingle<Genre>(sql, genreid);
        return genre;
    }
    
         public bool checkid(int genreid)
    {
        string sql = "SELECT * FROM genre where genre_id = @genreid";

        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, genreid);;
    }
     
              

     */
}