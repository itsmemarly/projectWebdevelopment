using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class StripboekRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }


    //gives back a specific stripboek using id
    public Stripboek Get(int stripboek_ID)
    {
        string sql = "SELECT * FROM stripboeken WHERE stripboek_id = @stripboek_ID";

        using var connection = GetConnection();
        var creator = connection.QuerySingle<Stripboek>(sql, new {stripboek_ID});
        return creator;
    }
    
    public bool checkid(int stripboek_ID)
    {
        string sql = "SELECT * FROM stripboeken WHERE stripboek_id = @stripboek_ID";

        using var connection = GetConnection();
        return connection.ExecuteScalar<bool>(sql, new {stripboek_ID});
    }

    //gives back the list of all stripboeken
    public IEnumerable<Stripboek> Get()
    {
        string sql = "SELECT * FROM stripboeken";

        using var connection = GetConnection();
        var stripboeken = connection.Query<Stripboek>(sql);
        return stripboeken;
    }

    //gives back a list of search result depending on titel using a string called search
    public IEnumerable<Stripboek> GetSearch(string search)
    {
        //search = "%" + search + "%";
        string sql = "SELECT * FROM stripboeken where titel like @search ";

        using var connection = GetConnection();
        var stripboeken = connection.Query<Stripboek>(sql,search);
        return stripboeken;
    }
    public bool checkSearch(string search)
    {
        //search = "%" + search + "%";
        string sql = "SELECT * FROM stripboeken where titel like @search ";

        using var connection = GetConnection();
        var check = connection.ExecuteScalar<bool>(sql,search);
        return check;
    }

    //adds a stripboek with al its variabkes to the database
    public Stripboek Add(Stripboek stripboek)
    {
        string sql = @"
                INSERT INTO stripboeken (isbn, uitgave1e_druk, reeks_nr, bladzijden, titel, expliciet, uitgever_id, reeks_id) 
                VALUES (@isbn, @Uitgave1e_druk, @Reeks_nr, @Bladzijden, @titel, @expleciet,@Uitgever_id, @Reeks_id); 
                SELECT * FROM stripboeken WHERE stripboek_id = LAST_INSERT_ID()";

        using var connection = GetConnection();
        var nieuwstripboek = connection.QuerySingle<Stripboek>(sql, stripboek);
        return nieuwstripboek;
    }

    //deletes a stripboek using id
    public bool Delete(int stripboek_id)
    {
        string sql = @"DELETE FROM stripboeken WHERE  stripboek_id = @stripboek_id";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new {stripboek_id});
        return numOfEffectedRows == 1;
    }

    //updates a stripboek their titel
    public Stripboek Update(Stripboek stripboek)
    {
        string sql = @"
                UPDATE stripboeken SET 
                    titel = @titel 
                WHERE stripboek_id = @Stripboek_id;
                SELECT * FROM stripboeken WHERE stripboek_id = @Stripboek_id";

        using var connection = GetConnection();
        var updatedStripboek = connection.QuerySingle<Stripboek>(sql, stripboek);
        return updatedStripboek;
    }
}