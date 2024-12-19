using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.TaskUser.Get;

namespace TaskManager.UnitTest.Application.Tasks;

[Collection(nameof(TaskFixtureApplicationCollection))]
public class GetTaskTest
{

    private readonly TaskFixtureApplication _fixture;

    public GetTaskTest(TaskFixtureApplication fixture)
    {
        _fixture = fixture;
    }

    // get task by id
    [Fact(DisplayName = nameof(GetTaskUseCase))]
    [Trait("Application", "Task - Entity")]
    public async Task GetTaskUseCase()
    {
        var (mockTaskRepository, task) = _fixture.GetTaskRepositoryMockWithTask();
        var useCase = new GetTask(mockTaskRepository.Object);
        var input = new GetTaskInput(task.Id);

        var output = await useCase.Handle(input, CancellationToken.None);

        mockTaskRepository.Verify(x => x.GetById(task.Id), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().Be(task.Id);
        output.Title.Should().Be(task.Title);
        output.Description.Should().Be(task.Description);
        output.Category.Should().Be(task.Category);

    }

    //throw exception when task not found
    [Fact(DisplayName = nameof(GetTaskUseCase_ThrowExceptionWhenTaskNotFound))]
    [Trait("Application", "Task - Entity")]
    public void GetTaskUseCase_ThrowExceptionWhenTaskNotFound()
    {
        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMockWithTask();
        var useCase = new GetTask(mockTaskRepository.Object);
        var input = new GetTaskInput(Guid.NewGuid());

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }

    // get task by with many tasks
    [Fact(DisplayName = nameof(GetTaskUseCase_WithManyTasks))]
    [Trait("Application", "Task - Entity")]
    public async Task GetTaskUseCase_WithManyTasks()
    {
        var (mockTaskRepository, tasks) = _fixture.GetTaskRepositoryMockWithTasks();
        var task = tasks.First();
        var useCase = new GetTask(mockTaskRepository.Object);
        var input = new GetTaskInput(task.Id);
        var output = await useCase.Handle(input, CancellationToken.None);

        //  mockTaskRepository.Verify(x => x.GetById(task.Id), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().Be(task.Id);
        output.Title.Should().Be(task.Title);
        output.Description.Should().Be(task.Description);
        output.Category.Should().Be(task.Category);


    }

    // throw exception when task not found with many tasks
    [Fact(DisplayName = nameof(ThrowWhenTaskNotFoundWithManyTask))]
    [Trait("Application", "Task - Entity")]
    public void ThrowWhenTaskNotFoundWithManyTask()
    {
        var (mockTaskRepository, tasks) = _fixture.GetTaskRepositoryMockWithTasks();
        var useCase = new GetTask(mockTaskRepository.Object);
        var input = new GetTaskInput(Guid.NewGuid());

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        act.Should().ThrowAsync<NotFoundException>();
    }
}
