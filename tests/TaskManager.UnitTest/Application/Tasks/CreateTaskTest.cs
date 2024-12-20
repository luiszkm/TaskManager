using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.TaskUser.Create;
using TaskManager.Domain.Exceptions;

namespace TaskManager.UnitTest.Application.Tasks;

[Collection(nameof(TaskFixtureApplicationCollection))]
public class CreateTaskTest
{

    private readonly TaskFixtureApplication _fixture;

    public CreateTaskTest(TaskFixtureApplication fixture)
    {
        _fixture = fixture;
    }

    // create task usecase
    [Fact(DisplayName = nameof(CreateTaskUseCase))]
    [Trait("Application", "Task - Entity")]

    public async void CreateTaskUseCase()
    {
        var repositoryMock = _fixture.GetTaskRepositoryMock();
        var (mock, user) = _fixture.GetUserRepositoryMockWithUser();
        var useCase = new CreateTask(repositoryMock.Object, mock.Object);
        var input = new CreateTaskInput(
            _fixture.GetTitle(),
            _fixture.GetDescription(),
            _fixture.GetCategory(),
            user.Id
            );

        var output = await useCase.Handle(input, CancellationToken.None);

        mock.Verify(x => x.GetById(user.Id), Times.Once);
        mock.Verify(x => x.Update(user), Times.Once);

        output.Id.Should().NotBeEmpty();
        output.UserId.Should().Be(user.Id);
        output.Title.Should().Be(input.Title);
        output.Description.Should().Be(input.Description);
        output.Category.Should().Be(input.Category);
        output.CreatedAt.Should().BeBefore(DateTime.UtcNow);
        output.IsCompleted.Should().BeFalse();

        user.Tasks.Should().NotBeEmpty();
        user.Tasks.Should().HaveCount(1);
    }

    // throw exception when user not found
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNotFound))]
    [Trait("Application", "Task - Entity")]
    public async void ThrowExceptionWhenUserNotFound()
    {
        var repositoryMock = _fixture.GetTaskRepositoryMock();
        var (mock, user) = _fixture.GetUserRepositoryMockWithUser(null);
        var useCase = new CreateTask(repositoryMock.Object, mock.Object);
        var input = _fixture.CreateValidTaskInput(Guid.NewGuid());

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
        user.Tasks.Should().BeEmpty();
    }

    // throw exception when Title is Invalid
    [Theory(DisplayName = nameof(ThrowExceptionWhenInputDataIsInvalid))]
    [Trait("Application", "Task - Entity")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("12")]
    [InlineData(null)]
    public async void ThrowExceptionWhenInputDataIsInvalid(string? title)
    {
        var repositoryMock = _fixture.GetTaskRepositoryMock();
        var (mock, user) = _fixture.GetUserRepositoryMockWithUser();
        var useCase = new CreateTask(repositoryMock.Object, mock.Object);
        var input = _fixture.CreateValidTaskInput(user.Id);
        input.Title = title;

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();
    }

    // throw exception when Description is Invalid
    [Theory(DisplayName = nameof(ThrowExceptionWhenDescriptionIsInvalid))]
    [Trait("Application", "Task - Entity")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("12")]
    [InlineData(null)]
    public async void ThrowExceptionWhenDescriptionIsInvalid(string? description)
    {
        var repositoryMock = _fixture.GetTaskRepositoryMock();
        var (mock, user) = _fixture.GetUserRepositoryMockWithUser();
        var useCase = new CreateTask(repositoryMock.Object, mock.Object);
        var input = _fixture.CreateValidTaskInput(user.Id);
        input.Description = description;

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();
        user.Tasks.Should().BeEmpty();
    }
}
