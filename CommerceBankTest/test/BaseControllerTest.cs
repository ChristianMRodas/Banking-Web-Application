using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
public abstract class BaseControllerTest
{
    public static DbConnection CreateInMemoryDatabase()
    {
        var connection = new SqliteConnection("Filename=:memory:");

        connection.Open();

        return connection;
    }
}