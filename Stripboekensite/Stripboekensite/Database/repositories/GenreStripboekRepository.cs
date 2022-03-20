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

    //gives back al the genres from a certain stripboek
    public IEnumerable<Genre> GetGenres(int stripboekid)
    {
        string sql = "SELECT * FROM genre_stripboeken where Stripboek_id = @stripboekid";

        using var connection = GetConnection();
        var genres = connection.Query<Genre>(sql, new{ stripboekid });
        return genres;
    }
    
    //gives back al the stripboeken from a certain genre
    public IEnumerable<Stripboek> GetStripboeken(int GenreId)
    {
        string sql = "SELECT * FROM genre_stripboeken where Genre_id = @GenreId";

        using var connection = GetConnection();
        var Stripboeken = connection.Query<Stripboek>(sql, GenreId);
        return Stripboeken;
    }

    //gives back a list of al genrestripboek
    public IEnumerable<GenreStripboek> Get()
    {
        string sql = "SELECT * FROM genre_Stripboeken";

        using var connection = GetConnection();
        var GenreStripboek = connection.Query<GenreStripboek>(sql);
        return GenreStripboek;
    }

    //adds a new genre stripboek combination to database
    public GenreStripboek Add(GenreStripboek GenreStripboek)
    {
        string sql = "INSERT INTO genre_Stripboeken (Genre_id,Stripboek_id) VALUES (@Genre_id,@Stripboek_id); SELECT * from genre_stripboeken WHERE Stripboek_id = @Stripboek_id and Genre_id = @Genre_id";
        using var connection = GetConnection();
        var newGenreStripboek = connection.QuerySingle<GenreStripboek>(sql, GenreStripboek);
        return newGenreStripboek;
    }

    //deletes a genre stripboek combination
    public bool Delete(int genreid, int stripboekid)
    {
        string sql = @"DELETE FROM genre_Stripboeken WHERE genre_id = @genreid and stripboek_id = @stripboekid";
        using var connection = GetConnection();
        var rowsaffected = connection.QuerySingle<int>(sql, new{ genreid, stripboekid });
        return rowsaffected == 1;
    }
}