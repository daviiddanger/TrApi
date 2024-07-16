using SQLite;
using System;
using System.IO;
using TrSinntecPrueba.Models;
using Xamarin.Forms;

public class DatabaseContext
{
    private static object locker = new object();
    private SQLiteConnection database;

    public DatabaseContext()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Sinntec.db3");
        database = new SQLiteConnection(dbPath);
        database.CreateTable<Frameworks>();
        database.CreateTable<Languages>();
    }

    public SQLiteConnection GetConnection()
    {
        return database;
    }
}
