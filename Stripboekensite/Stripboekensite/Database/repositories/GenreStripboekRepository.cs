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

    public void Delete(List<GenreStripboek> GenreStripboek)
    {
        string sql = @"DELETE FROM genre_Stripboeken WHERE genre_id = @Genre_id and stripboek_id = @Stripboek_id";
        List<GenreStripboek> empty= new List<GenreStripboek>();
        
        using var connection = GetConnection();
        foreach (var genreStripboek in GenreStripboek)
        {
            var rowsaffected = connection.Execute(sql, genreStripboek);

        }
        
    }
    
    //deletes a genre stripboek combination by genre id
    public int DeleteByGenreID(int Genre_id)
    {
        string sql = @"DELETE FROM genre_Stripboeken WHERE genre_id = @genreid";
        using var connection = GetConnection();
        return connection.Execute(sql, new{genreid = Genre_id});
    }
    
    //deletes a genre stripboek combination by book id
    public int DeleteByBookID(int Stripboek_id)
    {
        string sql = @"DELETE FROM genre_Stripboeken WHERE Stripboek_id = @book_id";
        using var connection = GetConnection();
        return connection.Execute(sql, new{book_id = Stripboek_id});
    }
    
}