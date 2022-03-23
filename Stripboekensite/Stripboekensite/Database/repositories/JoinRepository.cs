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
            splitOn: "stripboek_id").ToList();

        return GenreStripboeken;
    }

    public IEnumerable<Gebruikers_Stripboeken> joingebrstripboekstripboeken(int id)
    {
        var sql = @"select c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, p.Gebruikers_ID, p.stripboek_id, p.druk, p.uitgave, p.bandlengte, p.plaats_gekocht, p.prijs_gekocht, p.staat
                from gebruikers_stripboeken p 
                inner join stripboeken c on c.stripboek_id = p.stripboek_id where Gebruikers_ID = @id";
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
    
    
    /*does not get used
     
         //join method that return a list with al the stripboeken en creators
       public IEnumerable<CreatorStripboeken> Joincreatorstripboek()
    {
        var sql = @"select p.taak,c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, c.uitgever_id, c.reeks_id, e.creator_ID, e.creator_naam 
                from creators_stripboeken p
                inner join stripboeken c on p.stripboek_id = c.stripboek_id
                inner join creator e on p.creator_id = e.creator_ID";
        using var connection = GetConnection();
        var creators_stripboeken =connection.Query<CreatorStripboeken,Stripboek, Creator, CreatorStripboeken>(sql, (creator_stripboek, stripboek, creator) => {
                creator_stripboek.Stripboek = stripboek;
                creator_stripboek.Creator = creator;
                return creator_stripboek;
            }, 
            splitOn: "stripboek_id, creator_ID").ToList();

        return creators_stripboeken;
    }

    public IEnumerable<Stripboek> joinstripboek()
    {
        var sql = @"select c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, p.reeks_id, p.Reeks_titel, p.aantal, e.uitgever_id, e.Naam
                from stripboeken c
                inner join reeksen p on c.reeks_id = p.reeks_id
                inner join uitgever e on c.uitgever_id = e.uitgever_id";
        using var connection = GetConnection();
        var stripboeken =connection.Query<Stripboek,Reeks, Uitgever, Stripboek>(sql, (stripboek, reeks, uitgever) =>
            {
                stripboek.reeks = reeks;
                stripboek.uitgever = uitgever;
                return stripboek;
            }, 
            splitOn: "reeks_id, uitgever_id").ToList();

        return stripboeken;
    }
     
      public IEnumerable<Gebruikers_Stripboeken> joingebrstripboekstripboeken()
    {
        var sql = @"select c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, p.Gebruikers_ID, p.stripboek_id, p.druk, p.uitgave, p.bandlengte, p.plaats_gekocht, p.prijs_gekocht, p.staat
                from gebruikers_stripboeken p 
                inner join stripboeken c on c.stripboek_id = p.stripboek_id";
        using var connection = GetConnection();
        var stripboeken =connection.Query<Gebruikers_Stripboeken, Stripboek, Gebruikers_Stripboeken>(sql, (Gebruikers_Stripboek, stripboek) =>
            {
                Gebruikers_Stripboek.Stripboek = stripboek;
                return Gebruikers_Stripboek;
            }, 
            splitOn: "Stripboek_id").ToList();

        return stripboeken;
    }
     */
    
}