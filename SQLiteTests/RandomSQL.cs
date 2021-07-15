using System;
using Microsoft.Data.Sqlite;
using NUnit.Framework;

namespace Payroll.Tests.SQLiteTests
{
    public class RandomSQL : TestSqliteDB
    {

        [Test]
        public void GetVersion()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";

            var con = new SqliteConnection(cs);
            con.Open();

            var cmd = new SqliteCommand(stm, con);
            string version = cmd.ExecuteScalar().ToString();

            Console.WriteLine($"SQLite version: {version}");
        }

        // [Test]
        // public void TestCars()
        // {
        //     string cs = @"URI=file:/home/alex/RiderProjects/PaymentSystem/PayrollDB.sqlite";
        //
        //     SqliteConnection con = new SqliteConnection(cs);
        //     con.Open();
        //
        //     SqliteCommand cmd;
        //     cmd = new SqliteCommand(con);
        //
        //     cmd.CommandText = "DROP TABLE IF EXISTS cars";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = @"CREATE TABLE cars(id INTEGER PRIMARY KEY,
        //     name TEXT, price INT)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Audi',52642)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Mercedes',57127)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Skoda',9000)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volvo',29000)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Bentley',350000)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Citroen',21000)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Hummer',41400)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "INSERT INTO cars(name, price) VALUES('Volkswagen',21600)";
        //     cmd.ExecuteNonQuery();
        //
        //     cmd.CommandText = "SELECT * FROM cars";
        //
        //     var reader = cmd.ExecuteReader();
        //     string response = "";
        //
        //     while (reader.Read())
        //     {
        //         response += reader.GetInt16(0);
        //     }
        //
        //     Console.WriteLine("Table cars created" + response);
        // }
        
    }
}