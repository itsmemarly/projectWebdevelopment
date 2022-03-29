using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class JoinRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    public IEnumerable<GenreStripboek> joingenrestripboek()
    {
        var sql =
            @"select  e.genre_id, e.soort, p.stripboek_id, p.isbn, p.uitgave1e_druk, p.reeks_nr, p.bladzijden, p.titel, p.expliciet, p.uitgever_id, p.reeks_id
                from genre_stripboeken c
                inner join genre e on c.Genre_id = e.genre_id
                inner join stripboeken p on c.Stripboek_id = p.stripboek_id";
        using var connection = GetConnection();
        var GenreStripboeken = connection.Query<Genre, Stripboek, GenreStripboek>(sql, (Genre, Stripboek) =>
            {
                var genreStripboek = new GenreStripboek();
                genreStripboek.genre = Genre;
                genreStripboek.Stripboek = Stripboek;
                return genreStripboek;
            }, 
            splitOn: "stripboek_id");

        return GenreStripboeken;
    }

    public IEnumerable<GenreStripboek> joingenrestripboekid(int stripboekid)
    {
        var sql =
            @"select  e.genre_id, e.soort, p.stripboek_id, p.isbn, p.uitgave1e_druk, p.reeks_nr, p.bladzijden, p.titel, p.expliciet, p.uitgever_id, p.reeks_id
                from genre_stripboeken c
                inner join genre e on c.Genre_id = e.genre_id
                inner join stripboeken p on c.Stripboek_id = p.stripboek_id where p.stripboek_id = @stripboekid";
        using var connection = GetConnection();
        var GenreStripboeken = connection.Query<Genre, Stripboek, GenreStripboek>(sql, (Genre, Stripboek) =>
            {
                var genreStripboek = new GenreStripboek();
                genreStripboek.genre = Genre;
                genreStripboek.Stripboek = Stripboek;
                return genreStripboek;
            }, new {stripboekid}, 
            splitOn: "stripboek_id");

        return GenreStripboeken;
    }
    
    public IEnumerable<Gebruikers_Stripboeken> joingebrstripboekstripboeken(int id)
    {
        var sql = @"select  p.Gebruiker_stripboek_ID, p.druk, p.uitgave, p.bandlengte, p.plaats_gekocht, p.prijs_gekocht, p.staat, c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet
                from gebruikers_stripboeken p 
                left join stripboeken c on c.stripboek_id = p.stripboek_id where Gebruikers_ID = @id";
        using var connection = GetConnection();
        var stripboeken =connection.Query<Gebruikers_Stripboeken, Stripboek, Gebruikers_Stripboeken>(sql, (Gebruikers_Stripboek, stripboek) =>
            {
                Gebruikers_Stripboek.Stripboek = stripboek;
                return Gebruikers_Stripboek;
            }, 
            new {id}, 
            splitOn: "Stripboek_id");

        return stripboeken;
    }

    public IEnumerable<CreatorStripboeken> Joincreatorstripboek(int stripboekid)
    {
        var sql =
            @"select p.taak,c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, c.uitgever_id, c.reeks_id, e.creator_ID, e.creator_naam 
                from creators_stripboeken p
                left join stripboeken c on p.stripboek_id = c.stripboek_id
                left join creator e on p.creator_id = e.creator_ID where c.stripboek_id= @stripboekid";
        using var connection = GetConnection();
        var creators_stripboeken = connection.Query<CreatorStripboeken, Stripboek, Creator, CreatorStripboeken>(sql,
            (creator_stripboek, stripboek, creator) =>
            {
                creator_stripboek.Stripboek = stripboek;
                creator_stripboek.Creator = creator;
                return creator_stripboek;
            }, new {stripboekid},
            splitOn: "stripboek_id, creator_ID").ToList();

        return creators_stripboeken;
    }

    public Stripboek joinStripboek(int stripboekid)
    {
        var sql = @"select c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, p.reeks_id, p.Reeks_titel, p.aantal, e.uitgever_id, e.Naam
                from stripboeken c
                left join reeksen p on c.reeks_id = p.reeks_id
                left join uitgever e on c.uitgever_id = e.uitgever_id where c.stripboek_id = @stripboekid";
        using var connection = GetConnection();
        var stripboeken =connection.Query<Stripboek,Reeks, Uitgever, Stripboek>(sql, (stripboek, reeks, uitgever) =>
            {
                stripboek.reeks = reeks;
                stripboek.uitgever = uitgever;
                return stripboek;
            }, new {stripboekid}, 
            splitOn: "reeks_id, uitgever_id").ToList();

        return stripboeken[0];
    }

    public void DeleteStripBoekAndReferences(int stripboek_id)
    {
        string sqlGebruikersStripboeken = @"DELETE FROM gebruikers_stripboeken WHERE stripboek_id = @stripboek_id";
        string sqlGenreStripboeken = @"DELETE FROM genre_Stripboeken WHERE Stripboek_id = @stripboek_id";
        string sqlCreatorStripboeken = @"DELETE FROM creators_stripboeken WHERE stripboek_id = @stripboek_id";
        string sqlStripboeken = @"DELETE FROM stripboeken WHERE  stripboek_id = @stripboek_id";

        using var connection = GetConnection();
        
        connection.Execute(sqlGebruikersStripboeken, new {stripboek_id});
        connection.Execute(sqlGenreStripboeken, new {stripboek_id});
        connection.Execute(sqlCreatorStripboeken, new {stripboek_id});
        connection.Execute(sqlStripboeken, new {stripboek_id});
    }

    public void DeleteGenreAndReferences(int genre_id)
    {
        string sqlGenreStripboeken = @"DELETE FROM genre_Stripboeken WHERE genre_id = @genre_id";
        string sqlGenre = @"DELETE FROM genre WHERE genre_id = @genre_id";
        
        using var connection = GetConnection();
        
        connection.Execute(sqlGenreStripboeken, new{genre_id});
        connection.Execute(sqlGenre, new {genre_id});
    }

}