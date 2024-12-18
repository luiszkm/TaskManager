using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Repositories;

public interface IUserRepository : IBaseRepository
{
    Task UpdatePassword(Guid id, string password);
    Task<User?> GetByUserName(string userName);
    Task<List<User>> GetAll();

}
