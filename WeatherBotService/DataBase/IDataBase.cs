namespace WeatherBotService.DataBase;

public interface IDataBase
{
    public Task<List<IBotUser>> GetAllUsersAsync();
    public Task OpenConnectionAsync();
    public Task CloseConnectionAsync();
    public Task UpdateUserAsync(IBotUser user);
    public Task InsertUserAsync(IBotUser user);
    public Task DeleteUserAsync(IBotUser user);
    public Task CreateTableAsync();
    public Task<bool> CheckUserAsync(IBotUser user);
}
