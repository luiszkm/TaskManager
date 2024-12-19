namespace TaskManager.Domain.SeedWork;

public interface IBaseRepository<T> where T : Entity
{
    Task Create(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<T> GetById(Guid? id);

}
