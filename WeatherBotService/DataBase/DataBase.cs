using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
namespace WeatherBotService.DataBase;

public class DataBase : IDataBase
{
    private readonly SqliteConnection _sqliteConnection;
    public DataBase(IOptions<DataBaseOptions> options)
    {
        var value = options.Value;
        _sqliteConnection = new SqliteConnection($"Data Source={value.FileName}");
    }
    public async Task OpenConnectionAsync()
    {
        await _sqliteConnection.OpenAsync();
    }
    public async Task CreateTableAsync()
    {

        var sqlExpression =
        "CREATE TABLE IF NOT EXISTS 'Users' ("+
        "'Id' INTEGER NOT NULL UNIQUE,"+
        "'ChatId' INTEGER NOT NULL UNIQUE,"+
        "'Location' TEXT NOT NULL," +
        "'Language' TEXT NOT NULL," +
        "PRIMARY KEY('Id')"+
        ");";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        await command.ExecuteNonQueryAsync();
    }
    public async Task CloseConnectionAsync()
    {
        await _sqliteConnection.CloseAsync();
    }
    public async Task InsertUserAsync(IBotUser user)
    {
        var sqlExpression =
        "INSERT INTO 'Users' (ChatId, Location, Language)" +
        $"VALUES ({user.ChatId}, '{user.Location}', '{user.Language}');";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        await command.ExecuteNonQueryAsync();
    }
    public async Task UpdateUserAsync(IBotUser user)
    {
        var sqlExpression = $"UPDATE Users SET Location = {user.Location}, Language = {user.Language} WHERE Id = {user.ChatId};";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        await command.ExecuteNonQueryAsync();
    }
    public async Task DeleteUserAsync(IBotUser user)
    {
        var sqlExpression = $"DELETE FROM 'Users' WHERE ChatId={user.ChatId}";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        await command.ExecuteNonQueryAsync();
    }
    public async Task<List<IBotUser>> GetAllUsersAsync()
    {
        var sqlExpression = "SELECT ChatId, Location, Language FROM 'Users'";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        var users = new List<IBotUser>();;
        using var reader = await command.ExecuteReaderAsync();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                var chatId = reader.GetInt32(0);
                var location = reader.GetString(1);
                var language = reader.GetString(2);
                users.Add(new TelegramBotUser(chatId, location, language));
            }
        }
        return users;
    }
    public async Task<bool> CheckUserAsync(IBotUser user)
    {
        var sqlExpression = $"SELECT Count(*) FROM 'Users' WHERE ChatId={user.ChatId}";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        using var reader = await command.ExecuteReaderAsync();
        reader.Read();
        if (reader.GetInt32(0) > 0)
            return true;
        return false;
    }
}
