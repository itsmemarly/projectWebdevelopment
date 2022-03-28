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
    
    /* does not get used 
    
    //gives back specifiek creator stripboek combination
    public CreatorStripboeken Get(int Creator_ID, int Stripboek_ID)
    {
        string sql = "SELECT * FROM creators_stripboeken WHERE creator_ID = @Creator_ID and stripboek_id = @Stripboek_ID";

        using var connection = GetConnection();
        var creatorStripboeken = connection.QuerySingle<CreatorStripboeken>(sql, new { Creator_ID, Stripboek_ID});
        return creatorStripboeken;
    }

    //gives a list of al creator stripboek combination from certain creator
    public IEnumerable<CreatorStripboeken> GetStripboeks(int Creator_ID)
    {
        string sql = "SELECT * FROM creators_stripboeken WHERE creator_ID = @Creator_ID";

        using var connection = GetConnection();
        var Stripboeken  = connection.Query<CreatorStripboeken>(sql,Creator_ID);
        return Stripboeken;
    }
    
    //gives a list of al creator stripboek combination from certain stripboek
    public IEnumerable<CreatorStripboeken> GetCreators(int Stripboek_ID)
    {
        string sql = "SELECT * FROM creators_stripboeken where stripboek_id = @Stripboek_ID";

        using var connection = GetConnection();
        var creatorStripboeken  = connection.Query<CreatorStripboeken>(sql, Stripboek_ID);
        return creatorStripboeken;
    }

    //gives back the list of al creator stripboek combination
    public IEnumerable<CreatorStripboeken> Get()
    {
        string sql = "SELECT * FROM creators_stripboeken";

        using var connection = GetConnection();
        var creatorStripboeken  = connection.Query<CreatorStripboeken>(sql);
        return creatorStripboeken;
    }
    

    //deletes creator stripboek combination
    public bool Delete(int Creator_ID, int Stripboek_ID)
    {
        string sql = @"DELETE FROM creators_stripboeken WHERE creator_ID = @Creator_ID and stripboek_id = @Stripboek_ID";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { Creator_ID, Stripboek_ID});
        return numOfEffectedRows == 1;
    }
    
    //updates a creator taak in a creator stripboek combination
    public CreatorStripboeken Update(CreatorStripboeken creatorStripboeken)
    {
        string sql = @"
                UPDATE creators_stripboeken SET 
                    taak = @taak
                WHERE creator_ID = @Creator_id and stripboek_id = @Stripboek_id;
                SELECT * FROM creators_stripboeken WHERE creator_ID = @Creator_ID and stripboek_id = @Stripboek_ID";

        using var connection = GetConnection();
        var updatedcreatorStripboeken = connection.QuerySingle<CreatorStripboeken>(sql, creatorStripboeken);
        return updatedcreatorStripboeken;
    }
    */
}