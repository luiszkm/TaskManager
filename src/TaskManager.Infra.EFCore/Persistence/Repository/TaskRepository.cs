using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Domain.SeedWork;
using TaskManager.Infra.EFCore.Exceptions;

namespace TaskManager.Infra.EFCore.Persistence.Repository;

public class TaskRepository : ITasksRepository
{
    private readonly TaskManegDbContext _dbContext;


    public TaskRepository(TaskManegDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private DbSet<User> _user => _dbContext.Set<User>();
    private DbSet<TaskUser> _task => _dbContext.Set<TaskUser>();

    public Task<TaskUser> CompletetedTask(TaskUser task)
    {
        throw new NotImplementedException();
    }

    public async Task Create(TaskUser entity)
    {
        var user = await _user.FirstOrDefaultAsync(x => x.Id == entity.UserId);
        if (user == null) throw new NotFoundDBException("User not found");
        await _task.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(TaskUser entity)
    {
        var task = await _task.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (task == null) throw new NotFoundDBException("Task not found");
        _task.Remove(task);
        await _dbContext.SaveChangesAsync();

    }

    public async Task<List<TaskUser>> Filter(FilterInput? filterInput)
    {
        if (filterInput.Category != null)
        {
            return await _task.Where(x => x.Category == filterInput.Category).ToListAsync();
        }

        if (filterInput.UserId != null)
        {
            var user = await _user.FirstOrDefaultAsync(x => x.Id == filterInput.UserId);
            if (user == null) throw new NotFoundDBException("User not found");
            return await _task.Where(x => x.UserId == user.Id).ToListAsync();
        }

        if (filterInput.Category != null && filterInput.UserName != null)
        {
            var user = await _user.FirstOrDefaultAsync(x => x.UserName == filterInput.UserName);
            if (user == null) throw new NotFoundDBException("User not found");
            var tasks = await _task.Where(x => x.UserId == user.Id).ToListAsync();
            return tasks.Where(x => x.Category == filterInput.Category).ToList();
        }


        if (filterInput.UserName != null)
        {
            var user = await _user.FirstOrDefaultAsync(x => x.UserName == filterInput.UserName);
            if (user == null) throw new NotFoundDBException("User not found");
            return await _task.Where(x => x.UserId == user.Id).ToListAsync();
        }

        return await _task.ToListAsync();
    }


    public async Task<TaskUser> GetById(Guid id)
    {
        var task = await _task.FirstOrDefaultAsync(x => x.Id == id);
        if (task == null) throw new NotFoundDBException("Task not found");
        return task;
    }

    public async Task Update(TaskUser entity)
    {
        var task = await _task.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (task == null) throw new NotFoundDBException("Task not found");
        task.UpdateTask(
            entity.Title,
            entity.Description,
            entity.Category);
        _task.Update(task);
        await _dbContext.SaveChangesAsync();
    }
}
