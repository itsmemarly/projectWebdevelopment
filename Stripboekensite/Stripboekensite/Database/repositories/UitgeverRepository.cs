using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class UitgeverRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }

}