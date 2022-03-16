using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;


namespace Stripboekensite;

public class DbUtils : IDbUtils
{
    public IDbConnection GetDbConnection()
    { 
        string connectionString =
            Startup.StaticConfiguration.GetConnectionString("database_stripboeken.MySQL");

        return new MySqlConnection(connectionString);
    }
}

public interface IDbUtils
{
    IDbConnection GetDbConnection();
}