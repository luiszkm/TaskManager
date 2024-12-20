using Taskmanage.IntegrationTest.Base;
using TaskManager.Domain.Enums;
using TaskManager.Domain.SeedWork;

namespace Taskmanage.IntegrationTest.Infra.RepositoryTest.TaskRepository;

[CollectionDefinition(nameof(TaskReposytoryTestFixtureCollection))]
public class TaskReposytoryTestFixtureCollection : ICollectionFixture<TaskReposytoryTestFixture>
{

}


public class TaskReposytoryTestFixture : BaseFixture
{
    public FilterInput CreateValidFilterInput()
    {
        return new FilterInput();
    }


    public List<DomainEntity.User> CreateValidUsers(int? count = 5)
    {
        var users = new List<DomainEntity.User>();
        for (int i = 0; i < count; i++)
        {
            var user = CreateValidUser();
            users.Add(user);
        }
        return users;

    }


    public List<DomainEntity.TaskUser> CreateValidTaskUsers(List<DomainEntity.User> users)
    {
        var tasks = new List<DomainEntity.TaskUser>();
        foreach (var user in users)
        {
            var task = CreateValidTaskUser(user, GetCategory());
            tasks.Add(task);
        }
        return tasks;

    }

    public List<DomainEntity.TaskUser> CreateValidTaskUsersWithCategory(List<DomainEntity.User> users)
    {
        var tasks = new List<DomainEntity.TaskUser>();
        foreach (var user in users)
        {
            var task = CreateValidTaskUser(user, CategoryEnuns.Others);
            tasks.Add(task);
        }
        return tasks;

    }


    public List<DomainEntity.TaskUser> CreateValidTaskForCategory(List<DomainEntity.User> users)
    {
        var tasks = new List<DomainEntity.TaskUser>();
        foreach (var user in users)
        {
            var task = CreateValidTaskUser(user, category: CategoryEnuns.Personal);
            tasks.Add(task);
        }
        return tasks;

    }
}
