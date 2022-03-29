using System.Collections.Generic;
using System.Data;
using Dapper;


namespace Stripboekensite;

public class BewerkItemRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    public BewerkItem Get(int stripboek_id)
    {
        BewerkItem bewerkItem = new BewerkItem();
        string sql = "SELECT * FROM genre";

        using var connection = GetConnection();
        var genres  = connection.Query<Genre>(sql).ToList();
        
        sql = "SELECT * FROM reeksen";
        var reeksen  = connection.Query<Reeks>(sql).ToList();
        
        sql = "SELECT * FROM creator";
        var creators  = connection.Query<Creator>(sql).ToList();
        
        sql = "SELECT * FROM uitgever";
        var uitgevers  = connection.Query<Uitgever>(sql).ToList();

        sql = @"select c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, p.reeks_id, p.Reeks_titel, p.aantal, e.uitgever_id, e.Naam
                from stripboeken c
                left join reeksen p on c.reeks_id = p.reeks_id
                left join uitgever e on c.uitgever_id = e.uitgever_id where c.stripboek_id = @stripboek_id";
        var stripboeken =connection.Query<Stripboek,Reeks, Uitgever, Stripboek>(sql, (stripboek, reeks, uitgever) =>
            {
                stripboek.reeks = reeks;
                stripboek.uitgever = uitgever;
                return stripboek;
            }, new {stripboek_id}, 
            splitOn: "reeks_id, uitgever_id").ToList();
        
        sql = @"select p.taak,c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, c.uitgever_id, c.reeks_id, e.creator_ID, e.creator_naam 
                from creators_stripboeken p
                left join stripboeken c on p.stripboek_id = c.stripboek_id
                left join creator e on p.creator_id = e.creator_ID where c.stripboek_id= @stripboek_id";
        var creators_stripboeken = connection.Query<CreatorStripboeken, Stripboek, Creator, CreatorStripboeken>(sql,
            (creator_stripboek, stripboek, creator) =>
            {
                creator_stripboek.Stripboek = stripboek;
                creator_stripboek.Creator = creator;
                return creator_stripboek;
            }, new {stripboek_id},
            splitOn: "stripboek_id, creator_ID").ToList();

        
        sql = @"select  e.genre_id, e.soort, p.stripboek_id, p.isbn, p.uitgave1e_druk, p.reeks_nr, p.bladzijden, p.titel, p.expliciet, p.uitgever_id, p.reeks_id
                from genre_stripboeken c
                inner join genre e on c.Genre_id = e.genre_id
                inner join stripboeken p on c.Stripboek_id = p.stripboek_id where p.stripboek_id = @stripboek_id";
        var GenreStripboeken = connection.Query<Genre, Stripboek, GenreStripboek>(sql, (Genre, Stripboek) =>
            {
                var genreStripboek = new GenreStripboek();
                genreStripboek.genre = Genre;
                genreStripboek.Stripboek = Stripboek;
                return genreStripboek;
            }, new {stripboek_id}, 
            splitOn: "stripboek_id").ToList();

        bewerkItem.stripboek = stripboeken[0];
        bewerkItem.CreatorStripboeken = creators_stripboeken;
        bewerkItem.genrestripboeks = GenreStripboeken;
        bewerkItem.creators = creators;
        bewerkItem.uitgevers = uitgevers;
        bewerkItem.Genres = genres;
        bewerkItem.reeksen = reeksen;

        return bewerkItem;
    }
    
    
}