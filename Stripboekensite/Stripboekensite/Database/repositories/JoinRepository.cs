using System.Collections.Generic;
using System.Data;
using Dapper;

namespace Stripboekensite;

public class JoinRepository
{
    string sql = "SELECT Gebruiker_stripboek_ID, druk, uitgave, bandlengte, plaats_gekocht, prijs_gekocht, staat FROM gebruikers_stripboeken  INNER JOIN Gebruikers_stripboekenstripboeken INNER JOIN stripboeken";
    
    using var connection = GetConnection();
    {
        var gebruikers_stripboeken =
            await connection.QueryAsync<Gebruikers_Stripboeken, Stripboek, Gebruikers_Stripboeken>(sql,
                (Gebruikers_Stripboeken, Stripboek) =>
                {
                    Stripboekensite.Gebruikers_Stripboeken.stripboek.Add(Stripboek);
                    return Stripboekensite.Gebruikers_Stripboeken;
                }

    }
    foreach(var Gebruikers_Stripboeken in result)
    {
        Console.Write($"{Stripboekensite.Gebruikers_Stripboeken.Gebruiker_stripboek_ID}: ");
        foreach(var Stripboek in Stripboekensite.Gebruikers_Stripboeken.Stripboek)
        {
            Console.Write($" {Stripboek.stripboek_id} ");
        }
        Console.Write(Environment.NewLine);
    }
}
}