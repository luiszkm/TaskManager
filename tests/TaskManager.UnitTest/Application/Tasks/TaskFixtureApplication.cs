using TaskManager.Application.UseCases.TaskUser.Create;
using TaskManager.Application.UseCases.TaskUser.ListTasks;
using TaskManager.Application.UseCases.TaskUser.Update;
using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;
using TaskManager.UnitTest.Common;

namespace TaskManager.UnitTest.Application.Tasks;

[CollectionDefinition(nameof(TaskFixtureApplicationCollection))]
public class TaskFixtureApplicationCollection : ICollectionFixture<TaskFixtureApplication>
{
}
public class TaskFixtureApplication : TaskUserFixture
{
    public CreateTaskInput CreateValidTaskInput(Guid userId) =>
        new(GetTitle(), GetDescription(), GetCategory(), userId);


    public UpdateTaskInput CreateValidUpdateTaskInput(Guid taskId) =>
        new(taskId, GetTitle(), GetDescription(), GetCategory());

    public (Mock<IUserRepository>, DomainEntity.User) GetUserRepositoryMockWithUser(DomainEntity.User? userParam = null)
    {
        var user = userParam ?? GetValidUser();
        var mock = GetUserRepositoryMock();
        mock.Setup(x => x.GetById(user.Id))
            .ReturnsAsync(user);
        return (mock, user);
    }

    public (Mock<ITasksRepository>, DomainEntity.TaskUser)
        GetTaskRepositoryMockWithTask(DomainEntity.TaskUser? task = null, Guid? userId = null)
    {
        var taskUser = task ?? CreateValidTaskUser(userId);
        var mockTaskRepository = GetTaskRepositoryMock();
        mockTaskRepository.Setup(x => x.GetById(taskUser.Id))
            .ReturnsAsync(taskUser);
        return (mockTaskRepository, taskUser);
    }


    public (Mock<ITasksRepository>, List<DomainEntity.TaskUser>)
        GetTaskRepositoryMockWithTasks(DomainEntity.TaskUser? task = null, int? range = 10)
    {
        var tasks = Enumerable.Range(1, range ?? 10)
            .Select(_ => CreateValidTaskUser())
            .ToList();

        if (task != null)
        {
            tasks.Add(task);
        }

        var mockTaskRepository = GetTaskRepositoryMock();
        mockTaskRepository.Setup(x => x.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => tasks.FirstOrDefault(x => x.Id == id));

        return (mockTaskRepository, tasks);

    }


    public (Mock<ITasksRepository>, List<DomainEntity.TaskUser>)
        GetTaskRepositoryMocForFilter(
        DomainEntity.TaskUser? task = null,
        DomainEntity.User? user = null,
        int? range = 10,
        CategoryEnuns? category = null
        )

    {


        var tasks = Enumerable.Range(1, range ?? 10)
            .Select(_ => CreateValidTaskUser(user.Id, category))
            .ToList();

        if (task != null)
        {
            tasks.Add(task);
        }

        var mockTaskRepository = GetTaskRepositoryMock();
        mockTaskRepository.Setup(x => x.Filter(It.IsAny<ListTasksInput>()))
            .ReturnsAsync((ListTasksInput input) =>
            {
                var result = tasks.AsQueryable();

                if (input.Category != null)
                {
                    result = result.Where(x => x.Category == input.Category);
                }
                return result.ToList();
            });

        return (mockTaskRepository, tasks);

    }
}
