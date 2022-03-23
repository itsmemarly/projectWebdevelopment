using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class GenreStripboekRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }

    //adds a new genre stripboek combination to database
    public GenreStripboek Add(GenreStripboek GenreStripboek)
    {
        string sql = "INSERT INTO genre_Stripboeken (Genre_id,Stripboek_id) VALUES (@Genre_id,@Stripboek_id); SELECT * from genre_stripboeken WHERE Stripboek_id = @Stripboek_id and Genre_id = @Genre_id";
        using var connection = GetConnection();
        var newGenreStripboek = connection.QuerySingle<GenreStripboek>(sql, GenreStripboek);
        return newGenreStripboek;
    }
    
    public List<GenreStripboek> Add(List<GenreStripboek> GenreStripboek)
    {
        string sql = "INSERT INTO genre_Stripboeken (Genre_id,Stripboek_id) VALUES (@Genre_id,@Stripboek_id); SELECT * from genre_stripboeken WHERE Stripboek_id = @Stripboek_id and Genre_id = @Genre_id";
        using var connection = GetConnection();
        List<GenreStripboek> newGenreStripboeken= new List<GenreStripboek>();
        
        foreach (var genreStripboek in GenreStripboek)
        {
            var newGenreStripboek = connection.QuerySingle<GenreStripboek>(sql, genreStripboek);
            newGenreStripboeken.Add(newGenreStripboek);
        }
        return newGenreStripboeken;
    }

    
    /* does not get used
//gives back a list of al genrestripboek
    public IEnumerable<GenreStripboek> Get()
    {
        string sql = "SELECT * FROM genre_Stripboeken";

        using var connection = GetConnection();
        var GenreStripboek = connection.Query<GenreStripboek>(sql);
        return GenreStripboek;
    }


//deletes a genre stripboek combination
    public bool Delete(int genreid, int stripboekid)
    {
        string sql = @"DELETE FROM genre_Stripboeken WHERE genre_id = @genreid and stripboek_id = @stripboekid";
        using var connection = GetConnection();
        var rowsaffected = connection.QuerySingle<int>(sql, new{ genreid, stripboekid });
        return rowsaffected == 1;
    }
     */
}