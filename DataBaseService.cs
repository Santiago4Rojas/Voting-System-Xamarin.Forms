using JRojas;
using SQLite;
using System.Threading.Tasks;

public class DatabaseService
{
    readonly SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<User>().Wait();
    }

    // Guardar un nuevo usuario
    public Task<int> SaveUserAsync(User user)
    {
        return _database.InsertAsync(user);
    }

    // Obtener un usuario por nombre de usuario
    public Task<User> GetUserAsync(string username)
    {
        return _database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
    }
}