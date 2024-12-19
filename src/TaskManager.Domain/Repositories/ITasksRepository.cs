using TaskManager.Domain.Entities;
using TaskManager.Domain.SeedWork;

namespace TaskManager.Domain.Repositories;

public interface ITasksRepository : IBaseRepository<TaskUser>, IFilterRepository<TaskUser>
{
    Task<TaskUser> CompletetedTask(TaskUser task);

}
