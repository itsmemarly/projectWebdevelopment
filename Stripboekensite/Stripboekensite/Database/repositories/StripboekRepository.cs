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

    //gives back a list of search result depending on titel using a string called search
    public IEnumerable<Stripboek> GetSearch(string search, int searchtype)
    {
        string sql;
        using var connection = GetConnection();
        IEnumerable<Stripboek> stripboeken = new List<Stripboek>();
        
        if (searchtype == 1)
        {
            sql = "SELECT * FROM stripboeken where titel like @search";
        }
        else if (searchtype == 2)
        {
            //search on creator name
            sql =
                @"select c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, c.uitgever_id, c.reeks_id
                from creators_stripboeken p
                inner join stripboeken c on p.stripboek_id = c.stripboek_id
                inner join creator e on p.creator_id = e.creator_ID
                where creator_naam like @search";
        }
        else if (searchtype==3)
        {
            //search on reeks name
            sql = @"select c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, c.uitgever_id, c.reeks_id
                from stripboeken c
                inner join reeksen p on c.reeks_id = p.reeks_id 
                where Reeks_titel like @search";
        }
        else if (searchtype == 4)
        {
            //search on uitgever name
            sql = @"select c.stripboek_id, c.isbn, c.uitgave1e_druk, c.reeks_nr, c.bladzijden, c.titel, c.expliciet, c.uitgever_id, c.reeks_id
                from stripboeken c
                inner join uitgever e on c.uitgever_id = e.uitgever_id
                where Naam  like @search";
        }
        else
        {
            //fail save
            sql = "SELECT * FROM stripboeken";
        }
        
        stripboeken = connection.Query<Stripboek>(sql,new {search});
        return stripboeken;
    }

    //adds a stripboek with al its variabkes to the database
    public Stripboek Add(Stripboek stripboek)
    {
        string sql;
        if (stripboek.Reeks_id == 0)
        {
            sql = @"
                INSERT INTO stripboeken (isbn, uitgave1e_druk, bladzijden, titel, expliciet, uitgever_id) 
                VALUES (@isbn, @Uitgave1e_druk, @Bladzijden, @titel, @expleciet,@Uitgever_id); 
                SELECT * FROM stripboeken WHERE stripboek_id = LAST_INSERT_ID()";
        }
        else
        {
            sql = @" 
                INSERT INTO stripboeken (isbn, uitgave1e_druk, reeks_nr, bladzijden, titel, expliciet, uitgever_id, reeks_id) 
                VALUES (@isbn, @Uitgave1e_druk, @Reeks_nr, @Bladzijden, @titel, @expleciet,@Uitgever_id, @Reeks_id);
                
                SELECT * FROM stripboeken WHERE stripboek_id = LAST_INSERT_ID();";
        }

        using var connection = GetConnection();
        var nieuwstripboek = connection.QuerySingle<Stripboek>(sql, stripboek);
        return nieuwstripboek;
    }
    
    //gives back the list of all stripboeken
    public IEnumerable<Stripboek> Get()
    {
        string sql = "SELECT * FROM stripboeken";

        using var connection = GetConnection();
        var stripboeken = connection.Query<Stripboek>(sql);
        return stripboeken;
    }
    
    //updates a stripboek their titel
    public Stripboek Update(Stripboek stripboek)
    {
        string sql;
        if (stripboek.Reeks_id == 0)
        {
            sql = @"
                UPDATE stripboeken SET 
                    titel = @titel, isbn = @isbn, uitgave1e_druk= @Uitgave1e_druk, bladzijden=@Bladzijden, expliciet= @expleciet, uitgever_id =@Uitgever_id
                WHERE stripboek_id = @Stripboek_id;
                SELECT * FROM stripboeken WHERE stripboek_id = @Stripboek_id";
        }
        else
        {
            sql = @"
                UPDATE stripboeken SET 
                    titel = @titel, isbn = @isbn,reeks_id = @Reeks_id, reeks_nr = @reeks_nr, uitgave1e_druk= @Uitgave1e_druk, bladzijden=@Bladzijden, expliciet= @expleciet, uitgever_id =@Uitgever_id
                WHERE stripboek_id = @Stripboek_id;
                SELECT * FROM stripboeken WHERE stripboek_id = @Stripboek_id";
        }

        using var connection = GetConnection();
        var updatedStripboek = connection.QuerySingle<Stripboek>(sql, stripboek);
        return updatedStripboek;
    }
    
    //deletes a stripboek using id
    public bool Delete(int stripboek_id)
    {
        string sql = @"DELETE FROM stripboeken WHERE  stripboek_id = @stripboek_id";

        using var connection = GetConnection();
        int numOfEffectedRows = connection.Execute(sql, new {stripboek_id});
        return numOfEffectedRows == 1;
    }

    /*does not get used
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
     
     */
}