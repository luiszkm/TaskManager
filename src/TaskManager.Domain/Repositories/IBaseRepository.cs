namespace TaskManager.Domain.Repositories;

public interface IBaseRepository
{
    Task Create<T>(T entity) where T : class;
    Task Update<T>(T entity) where T : class;
    Task Delete<T>(T entity) where T : class;
    Task<T> GetById<T>(Guid id) where T : class;

}
