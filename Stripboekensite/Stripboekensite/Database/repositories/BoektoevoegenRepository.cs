using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class BoektoevoegenRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }
    
    // add new books to database
    
    // get existing stripboek_id
    // add isbn
    // add uitgave1e_druk
    // add reeks_nr
    // add bladzijden
    // add titel
    // add expliciet true or false
    // add uitgever_id
    // add reeks_id
    // update database


}