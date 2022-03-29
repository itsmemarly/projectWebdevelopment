using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class CreatorStripboekenRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    //adds a new creator stripboek combination to database
    public CreatorStripboeken Add(CreatorStripboeken creatorStripboeken) 
    {
        string sql = @"
                INSERT INTO creators_stripboeken (stripboek_id, creator_id, taak) 
                VALUES (@Stripboek_id, @Creator_id, @taak);
                SELECT * FROM creators_stripboeken WHERE creators_stripboek_id= LAST_INSERT_ID();";

        using var connection = GetConnection();
        var nieuwcreatorStripboeken = connection.QuerySingle<CreatorStripboeken>(sql, creatorStripboeken);
        return nieuwcreatorStripboeken;
    }
    
    //deletes creator stripboek combination
    public bool Delete(int Creator_ID, int Stripboek_ID)
    {
        string sql = @"DELETE FROM creators_stripboeken WHERE creator_ID = @Creator_ID and stripboek_id = @Stripboek_ID";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { Creator_ID, Stripboek_ID});
        return numOfEffectedRows == 1;
    }
    
    //deletes creator stripboek combination
    public bool DeleteByStripboek(int Stripboek_ID)
    {
        string sql = @"DELETE FROM creators_stripboeken WHERE stripboek_id = @Stripboek_ID";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new {Stripboek_ID});
        return numOfEffectedRows == 1;
    }
    
}