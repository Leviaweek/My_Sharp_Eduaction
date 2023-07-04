namespace WeatherBotService.DataBase;

public interface IDataBase<T>
{
    public Task<List<T>> GetAllUsersAsync(CancellationToken cancellationToken=default);
    public Task OpenConnectionAsync(CancellationToken cancellationToken=default);
    public Task CloseConnectionAsync(CancellationToken cancellationToken=default);
    public Task UpdateUserAsync(T obj, CancellationToken cancellationToken=default);
    public Task InsertUserAsync(T obj, CancellationToken cancellationToken=default);
    public Task DeleteUserAsync(T obj, CancellationToken cancellationToken=default);
    public Task CreateTableAsync(CancellationToken cancellationToken=default);
    public Task<bool> CheckUserAsync(T obj, CancellationToken cancellationToken=default);
}
