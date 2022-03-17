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

      
    //geeft speciefiek stripboek terug op basis id 
    public Stripboek Get(int stripboek_ID)
    {
        string sql = "SELECT * FROM stripboeken WHERE stripboek_id = @stripboek_ID";

        using var connection = GetConnection();
        var creator = connection.QuerySingle<Stripboek>(sql, new { stripboek_ID });
        return creator;
    }

    //geeft een IEnumerable lijst terug met stripboek.
    public IEnumerable<Stripboek> Get()
    {
        string sql = "SELECT * FROM Creator";

        using var connection = GetConnection();
        var stripboeken  = connection.Query<Stripboek>(sql);
        return stripboeken;
    }
    
    //voegt nieuwe stripboek toe (met alle variabelen kunnen minder van gemaakt worden als dat moet)
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
    
    //verwijdert stripboek
    public bool Delete(int stripboek_id)
    {
        string sql = @"DELETE FROM stripboeken WHERE  stripboek_id = @stripboek_id";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new { stripboek_id });
        return numOfEffectedRows == 1;
    }
    //updates een stripboek zijn titel
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