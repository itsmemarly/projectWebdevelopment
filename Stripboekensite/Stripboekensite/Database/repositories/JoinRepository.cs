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
    
    
    
}