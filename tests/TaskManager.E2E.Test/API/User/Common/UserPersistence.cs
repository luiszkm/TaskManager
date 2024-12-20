using Microsoft.EntityFrameworkCore;
using TaskManager.Infra.EFCore;

namespace TaskManager.E2E.Test.API.User.Common;

public class UserPersistence
{
    private readonly TaskManegDbContext _context;

    public UserPersistence(TaskManegDbContext context)
    {
        _context = context;
    }

    public async Task<DomainEntity.User?> GetById(Guid id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }


    public async Task InsertList(List<DomainEntity.User> users)
    {
        await _context.Users.AddRangeAsync(users);
        await _context.SaveChangesAsync();
    }

    public async Task<DomainEntity.User> InsertUser(DomainEntity.User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        // Garantir que o usuário foi realmente salvo
        var savedUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == user.Id);


        if (savedUser == null)
        {
            throw new InvalidOperationException("Usuário não encontrado após inserção.");
        }

        return savedUser;
    }




}
