using TaskManager.Application.UseCases.TaskUser.Update;

namespace TaskManager.UnitTest.Application.Tasks;

[Collection(nameof(TaskFixtureApplicationCollection))]
public class UpdateTaskTest
{
    private readonly TaskFixtureApplication _fixture;

    public UpdateTaskTest(TaskFixtureApplication fixture)
    {
        _fixture = fixture;
    }

    // update task usecase
    [Fact(DisplayName = nameof(UpdateTaskUseCase))]
    [Trait("Application", "Task - Entity")]
    public async void UpdateTaskUseCase()
    {
        var (mockTaskRepository, task) = _fixture.GetTaskRepositoryMockWithTask();
        var useCase = new UpdateTask(mockTaskRepository.Object);
        var input = _fixture.CreateValidUpdateTaskInput(task.Id);

        var output = await useCase.Handle(input, CancellationToken.None);

        mockTaskRepository.Verify(x => x.Update(task), Times.Once);

        output.Id.Should().Be(task.Id);
        output.UserId.Should().Be(task.UserId);
        output.Title.Should().Be(input.Title);
        output.Description.Should().Be(input.Description);
        output.Category.Should().Be(input.Category);
        output.CreatedAt.Should().Be(task.CreatedAt);
        output.IsCompleted.Should().Be(task.IsCompleted);
    }

    // update task usecase only title
    [Fact(DisplayName = nameof(UpdateTaskUseCaseOnlyTitle))]
    [Trait("Application", "Task - Entity")]
    public async void UpdateTaskUseCaseOnlyTitle()
    {
        var (mockTaskRepository, task) = _fixture.GetTaskRepositoryMockWithTask();
        var useCase = new UpdateTask(mockTaskRepository.Object);
        var input = _fixture.CreateValidUpdateTaskInput(task.Id);
        input.Description = null;
        input.Category = null;

        var output = await useCase.Handle(input, CancellationToken.None);

        mockTaskRepository.Verify(x => x.Update(task), Times.Once);

        output.Id.Should().Be(task.Id);
        output.UserId.Should().Be(task.UserId);
        output.Title.Should().Be(input.Title);
        output.Description.Should().Be(task.Description);
        output.Category.Should().Be(task.Category);
        output.CreatedAt.Should().Be(task.CreatedAt);
        output.IsCompleted.Should().Be(task.IsCompleted);
    }

    // update task usecase only description
    [Fact(DisplayName = nameof(UpdateTaskUseCaseOnlyDescription))]
    [Trait("Application", "Task - Entity")]
    public async void UpdateTaskUseCaseOnlyDescription()
    {
        var (mockTaskRepository, task) = _fixture.GetTaskRepositoryMockWithTask();
        var useCase = new UpdateTask(mockTaskRepository.Object);
        var input = _fixture.CreateValidUpdateTaskInput(task.Id);
        input.Title = null;
        input.Category = null;

        var output = await useCase.Handle(input, CancellationToken.None);

        mockTaskRepository.Verify(x => x.Update(task), Times.Once);

        output.Id.Should().Be(task.Id);
        output.UserId.Should().Be(task.UserId);
        output.Title.Should().Be(task.Title);
        output.Description.Should().Be(input.Description);
        output.Category.Should().Be(task.Category);
        output.CreatedAt.Should().Be(task.CreatedAt);
        output.IsCompleted.Should().Be(task.IsCompleted);
    }

    // update task usecase only category
    [Fact(DisplayName = nameof(UpdateTaskUseCaseOnlyCategory))]
    [Trait("Application", "Task - Entity")]
    public async void UpdateTaskUseCaseOnlyCategory()
    {
        var (mockTaskRepository, task) = _fixture.GetTaskRepositoryMockWithTask();
        var useCase = new UpdateTask(mockTaskRepository.Object);
        var input = _fixture.CreateValidUpdateTaskInput(task.Id);
        input.Title = null;
        input.Description = null;

        var output = await useCase.Handle(input, CancellationToken.None);

        mockTaskRepository.Verify(x => x.Update(task), Times.Once);

        output.Id.Should().Be(task.Id);
        output.UserId.Should().Be(task.UserId);
        output.Title.Should().Be(task.Title);
        output.Description.Should().Be(task.Description);
        output.Category.Should().Be(input.Category);
        output.CreatedAt.Should().Be(task.CreatedAt);
        output.IsCompleted.Should().Be(task.IsCompleted);
    }



}
