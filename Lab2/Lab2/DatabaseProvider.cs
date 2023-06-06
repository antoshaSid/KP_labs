using Microsoft.VisualBasic;
using SQLite;
using FileSystem = Microsoft.Maui.Storage.FileSystem;

namespace Lab2;

public static class DatabaseProvider
{
    public const string DatabaseFilename = "sqlite-database.db3";
    private static SQLiteAsyncConnection Database;

    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath => Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), DatabaseFilename);

    public static async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(DatabasePath, Flags);
        var result = await Database.CreateTableAsync<ToDoItem>();
        Console.WriteLine(DatabasePath);
    }

    public static async Task SaveItemAsync(ToDoItem item)
    {
        await Database.InsertAsync(item);
    }
}

public class ToDoItem
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public bool completed { get; set; }
}