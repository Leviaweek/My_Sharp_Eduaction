using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
namespace WeatherBotService.DataBase;

public class DataBase : IDataBase<TelegramBotUser>, IAsyncDisposable
{
    private readonly SqliteConnection _sqliteConnection;
    private bool _isOpened = false;
    public DataBase(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        _sqliteConnection = new SqliteConnection($"Data Source={connectionString}");
    }
    public async Task OpenConnectionAsync(CancellationToken cancellationToken = default)
    {
        await _sqliteConnection.OpenAsync(cancellationToken);
        _isOpened = true;
        await CreateTableAsync(cancellationToken);
    }
    public async Task CreateTableAsync(CancellationToken cancellationToken = default)
    {

        var sqlExpression ="""
        CREATE TABLE IF NOT EXISTS 'Users'(
        'Id' INTEGER NOT NULL UNIQUE,
        'ChatId' INTEGER NOT NULL UNIQUE,
        'Location' TEXT NOT NULL,
        'Language' TEXT NOT NULL,
        PRIMARY KEY('Id')
        );
        """;
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
    public async Task CloseConnectionAsync(CancellationToken cancellationToken = default)
    {
        await _sqliteConnection.CloseAsync();
    }
    public async Task InsertUserAsync(TelegramBotUser user, CancellationToken cancellationToken = default)
    {
        var sqlExpression = """
        INSERT INTO 'Users' (ChatId, Location, Language)
        VALUES (@ChatId, @Location, @Language);
        """;
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        command.Parameters.AddWithValue("@ChatId", user.ChatId);
        command.Parameters.AddWithValue("@Location", user.Location);
        command.Parameters.AddWithValue("@Language", user.Language);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
    public async Task UpdateUserAsync(TelegramBotUser user, CancellationToken cancellationToken = default)
    {
        var sqlExpression = $"UPDATE Users SET Location = @Location, Language = @Language WHERE Id = @ChatId;";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        command.Parameters.AddWithValue("@ChatId", user.ChatId);
        command.Parameters.AddWithValue("@Location", user.Location);
        command.Parameters.AddWithValue("@Language", user.Language);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
    public async Task DeleteUserAsync(TelegramBotUser user, CancellationToken cancellationToken = default)
    {
        var sqlExpression = $"DELETE FROM 'Users' WHERE ChatId=@ChatId";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        command.Parameters.AddWithValue("@ChatId", user.ChatId);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
    public async Task<List<TelegramBotUser>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var sqlExpression = "SELECT ChatId, Location, Language FROM 'Users'";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        var users = new List<TelegramBotUser>();;
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
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
    public async Task<bool> CheckUserAsync(TelegramBotUser user, CancellationToken cancellationToken = default)
    {
        var sqlExpression = $"SELECT Count(*) FROM 'Users' WHERE ChatId=@ChatId";
        var command = new SqliteCommand(sqlExpression, _sqliteConnection);
        command.Parameters.AddWithValue("@ChatId", user.ChatId);
        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        reader.Read();
        if (reader.GetInt32(0) > 0)
            return true;
        return false;
    }
    public async ValueTask DisposeAsync()
    {
        if (_isOpened)
        {
            await CloseConnectionAsync();

            _isOpened = false;
        }
        GC.SuppressFinalize(this);
    }
}
