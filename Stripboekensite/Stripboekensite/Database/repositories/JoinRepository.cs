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
    
    //join method that return a list with al the stripboeken en creators
    public IEnumerable<CreatorStripboeken> JoinBoektoCreatStripb()
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
    
    //join method that returns a list with all stripboeken en gebruikers_stripboeken
    public IEnumerable<Gebruikers_Stripboeken> JoinBoektoGebruikerStripboeken()
    {
        var sql = @"select p.Gebruiker_stripboek_ID, p.stripboek_id, p.Gebruikers_ID, p.druk, p.uitgave, p.bandlengte, p.plaats_gekocht, p.prijs_gekocht, p.staat, c. stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, c.uitgever_id, c.reeks_id 
                from gebruikers_stripboeken p
                inner join stripboeken c on p.stripboek_id = c.stripboek_id";
        using var connection = GetConnection();
        var gebruikers_stripboeken  = connection.Query<Gebruikers_Stripboeken, Gebruiker, Stripboek>(sql, map:(gebruikers_stripboeken:Gebruikers_Stripboeken, stripboek) => {
                gebruikers_stripboeken.Stripboek = stripboek;
                return gebruikers_stripboeken;
            },
            splitOn: "Gebruiker_stripboek_ID, stripboek_ID").ToList();

            return gebruikers_stripboeken;
    }
    