using TaskManager.Domain.Enums;
using TaskManager.Domain.SeedWork;

namespace Taskmanage.IntegrationTest.Infra.RepositoryTest.TaskRepository;

[Collection(nameof(TaskReposytoryTestFixtureCollection))]
public class TaskReposytoryTest
{
    private readonly TaskReposytoryTestFixture _fixture;

    public TaskReposytoryTest(TaskReposytoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    // Insert Task
    [Fact(DisplayName = nameof(InsertTask))]
    [Trait("Integration", "Task - repository")]
    public async Task InsertTask()
    {
        var user = _fixture.CreateValidUser();
        var task = _fixture.CreateValidTaskUser(user);
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);

        await repository.Create(task);
        var userDb = await dbContext.Users.FindAsync(user.Id);
        dbContext.Tasks.Should().Contain(task);
        userDb.Tasks.Should().Contain(task);

    }

    // throw when user not found
    [Fact(DisplayName = nameof(ThrowWhenUserNotFound))]
    [Trait("Integration", "Task - repository")]
    public async Task ThrowWhenUserNotFound()
    {
        var user = _fixture.CreateValidUser();
        var task = _fixture.CreateValidTaskUser(user);
        var dbContext = _fixture.GetDbContext();
        var repository = new Repository.TaskRepository(dbContext);

        Func<Task> act = async () => await repository.Create(task);

        await act.Should().ThrowAsync<Exception>();
    }

    // Get Task ById
    [Fact(DisplayName = nameof(GetTaskById))]
    [Trait("Integration", "Task - repository")]

    public async Task GetTaskById()
    {
        // Arrange
        var user = _fixture.CreateValidUser();
        var task = _fixture.CreateValidTaskUser(user);
        var dbContext = _fixture.GetDbContext(true);

        // Insere o usuário no banco
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        // Insere a tarefa no banco
        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();

        var repository = new Repository.TaskRepository(dbContext);

        // Act
        var result = await repository.GetById(task.Id);

        // Assert
        result.Should().BeEquivalentTo(task);
    }

    // throw when task not found
    [Fact(DisplayName = nameof(ThrowWhenTaskNotFound))]
    [Trait("Integration", "Task - repository")]
    public async Task ThrowWhenTaskNotFound()
    {
        var user = _fixture.CreateValidUser();
        var task = _fixture.CreateValidTaskUser(user);
        var dbContext = _fixture.GetDbContext();
        var repository = new Repository.TaskRepository(dbContext);

        Func<Task> act = async () => await repository.GetById(task.Id);

        await act.Should().ThrowAsync<Exception>();
    }

    // Update Task
    [Fact(DisplayName = nameof(UpdateTask))]
    [Trait("Integration", "Task - repository")]
    public async Task UpdateTask()
    {
        var user = _fixture.CreateValidUser();
        var task = _fixture.CreateValidTaskUser(user);
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);
        var title = _fixture.GetTitle();
        var description = _fixture.GetDescription();
        var category = _fixture.GetCategory();

        task.UpdateTask(
            title,
            description,
            category
            );

        await repository.Update(task);

        var result = await repository.GetById(task.Id);

        result.Should().BeEquivalentTo(task);
    }

    // throw when task not found
    [Fact(DisplayName = nameof(ThrowWhenTaskNotFoundUpdate))]
    [Trait("Integration", "Task - repository")]
    public async Task ThrowWhenTaskNotFoundUpdate()
    {
        var user = _fixture.CreateValidUser();
        var task = _fixture.CreateValidTaskUser(user);
        var dbContext = _fixture.GetDbContext();
        var repository = new Repository.TaskRepository(dbContext);

        Func<Task> act = async () => await repository.Update(task);

        await act.Should().ThrowAsync<Exception>();
    }

    // Delete Task
    [Fact(DisplayName = nameof(DeleteTask))]
    [Trait("Integration", "Task - repository")]
    public async Task DeleteTask()
    {
        var user = _fixture.CreateValidUser();
        var task = _fixture.CreateValidTaskUser(user);
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
        await dbContext.Tasks.AddAsync(task);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);

        await repository.Delete(task);

        var result = await dbContext.Tasks.FindAsync(task.Id);
        var userDb = await dbContext.Users.FindAsync(user.Id);
        result.Should().BeNull();
        userDb.Tasks.Should().NotContain(task);
    }

    // filter task withoutFilter
    [Fact(DisplayName = nameof(FilterTaskWithoutFilter))]
    [Trait("Integration", "Task - repository")]
    public async Task FilterTaskWithoutFilter()
    {
        var users = _fixture.CreateValidUsers(5);

        var task = _fixture.CreateValidTaskUsers(users);
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
        await dbContext.Tasks.AddRangeAsync(task);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);
        var input = _fixture.CreateValidFilterInput();
        var result = await repository.Filter(input);

        result.Should().Contain(task);
        result.Should().HaveCount(5);
    }

    // filter task by category
    [Fact(DisplayName = nameof(FilterTaskByCategory))]
    [Trait("Integration", "Task - repository")]
    public async Task FilterTaskByCategory()
    {
        var users = _fixture.CreateValidUsers(5);

        var task = _fixture.CreateValidTaskForCategory(users); // all tasks for category personal
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
        await dbContext.Tasks.AddRangeAsync(task);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);
        var input = new FilterInput(
            category: CategoryEnuns.Study
            );


        var updateTask = await dbContext.Tasks.FindAsync(task.First().Id);
        updateTask.UpdateTask(category: CategoryEnuns.Study);

        dbContext.Tasks.Update(updateTask);
        await dbContext.SaveChangesAsync();

        var result = await repository.Filter(input);

        result.Should().Contain(task.First());
        result.Should().HaveCount(1);
    }

    // filter task by userName
    [Fact(DisplayName = nameof(FilterTaskByUserName))]
    [Trait("Integration", "Task - repository")]
    public async Task FilterTaskByUserName()
    {

        var users = _fixture.CreateValidUsers(5);
        var user = _fixture.CreateValidUser();

        users.Add(user);
        var tasks = _fixture.CreateValidTaskUsers(users);
        var task = _fixture.CreateValidTaskUser(user);
        tasks.Add(task);
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
        await dbContext.Tasks.AddRangeAsync(tasks);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);
        var input = new FilterInput(
                       userName: user.UserName
                                  );

        var result = await repository.Filter(input);

        result.Should().Contain(tasks.First());
        result.Should().HaveCount(1);
    }
    // filter task by  userName and category

    [Fact(DisplayName = nameof(FilterTaskByUserNameAndCategory))]
    [Trait("Integration", "Task - repository")]

    public async Task FilterTaskByUserNameAndCategory()
    {
        var users = _fixture.CreateValidUsers(5);
        var user = _fixture.CreateValidUser();
        users.Add(user);
        var tasks = _fixture.CreateValidTaskUsersWithCategory(users);
        var task = _fixture.CreateValidTaskUserForFilter(user, category: CategoryEnuns.Work);
        tasks.Add(task);
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
        await dbContext.Tasks.AddRangeAsync(tasks);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);
        var input = new FilterInput(
                       userName: user.UserName,
                      category: CategoryEnuns.Work);

        var result = await repository.Filter(input);

        result.Should().HaveCount(1);
    }

    // get task by userId
    [Fact(DisplayName = nameof(GetTaskByUserId))]
    [Trait("Integration", "Task - repository")]
    public async Task GetTaskByUserId()
    {
        var users = _fixture.CreateValidUsers(5);
        var user = _fixture.CreateValidUser();
        users.Add(user);
        var tasks = _fixture.CreateValidTaskUsers(users);
        var task = _fixture.CreateValidTaskUser(user);
        tasks.Add(task);
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
        await dbContext.Tasks.AddRangeAsync(tasks);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);
        var input = new FilterInput(userId: user.Id);

        var result = await repository.Filter(input);

        result.Should().Contain(task);
        result.Should().HaveCount(1);
    }

    // filter task by userId and category
    [Fact(DisplayName = nameof(FilterTaskByUserIdAndCategory))]
    [Trait("Integration", "Task - repository")]
    public async Task FilterTaskByUserIdAndCategory()
    {
        var users = _fixture.CreateValidUsers(5);
        var user = _fixture.CreateValidUser();
        users.Add(user);
        var tasks = _fixture.CreateValidTaskUsersWithCategory(users);
        var task = _fixture.CreateValidTaskUserForFilter(user, category: CategoryEnuns.Work);
        tasks.Add(task);
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
        await dbContext.Tasks.AddRangeAsync(tasks);
        await dbContext.SaveChangesAsync();
        var repository = new Repository.TaskRepository(dbContext);
        var input = new FilterInput(
                                  userId: user.Id,
                                                       category: CategoryEnuns.Work);

        var result = await repository.Filter(input);

        result.Should().HaveCount(1);
    }
}
