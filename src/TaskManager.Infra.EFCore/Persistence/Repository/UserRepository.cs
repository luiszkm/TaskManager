using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Repositories;
using TaskManager.Infra.EFCore.Exceptions;

namespace TaskManager.Infra.EFCore.Persistence.Repository;

public class UserRepository : IUserRepository
{
    private readonly TaskManegDbContext _dbContext;
    private DbSet<User> _user => _dbContext.Set<User>();
    public UserRepository(TaskManegDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task Create(User entity)
    {
        var veryfyUser = await _user.FirstOrDefaultAsync(x => x.UserName == entity.UserName);
        if (veryfyUser != null)
            throw new BadRequestDbException("User already exists");

        await _user.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public Task Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetAll()
    {
        var users = await _user.ToListAsync();
        return users;
    }
    public async Task<User> GetById(Guid id)
    {
        var user = await _user.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) throw new NotFoundDBException("User not found In Db");
        return user;
    }
    public async Task<User> GetByUserName(string userName)
    {
        var user = await _user.FirstOrDefaultAsync(x => x.UserName == userName);
        if (user == null) throw new NotFoundDBException("User not found In DB");
        return user;

    }

    public async Task Update(User entity)
    {
        var user = await _user.FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (user == null) throw new Exception("User not found");
        _user.Update(entity);
        await _dbContext.SaveChangesAsync();
    }


}
