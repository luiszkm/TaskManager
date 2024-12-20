using TaskManager.Domain.Entities;
using TaskManager.Domain.SeedWork;

namespace TaskManager.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByUserName(string userName);
    Task<List<User>> GetAll();

    // chamge password


}
