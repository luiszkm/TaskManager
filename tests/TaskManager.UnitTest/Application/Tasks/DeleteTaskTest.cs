
using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.TaskUser.Delete;

namespace TaskManager.UnitTest.Application.Tasks;

[Collection(nameof(TaskFixtureApplicationCollection))]
public class DeleteTaskTest
{
    private readonly TaskFixtureApplication _fixture;

    public DeleteTaskTest(TaskFixtureApplication fixture)
    {
        _fixture = fixture;
    }

    // delete task
    [Fact(DisplayName = nameof(DeleteTaskUseCase))]
    [Trait("Application", "Task - Entity")]
    public async void DeleteTaskUseCase()
    {
        var (mock, user) = _fixture.GetUserRepositoryMockWithUser();
        var userTask = _fixture.CreateValidTaskUser(user.Id);
        var (repositoryMock, task) = _fixture.GetTaskRepositoryMockWithTask(userTask);

        var useCase = new DeleteTask(repositoryMock.Object, mock.Object);
        var input = new DeleteTaskInput(task.Id);

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.GetById(task.Id), Times.Once);
        repositoryMock.Verify(x => x.Delete(task), Times.Once);
        mock.Verify(x => x.Update(user), Times.Once);
        mock.Verify(x => x.GetById(user.Id), Times.Once);

        user.Tasks.Should().BeEmpty();
        user.Tasks.Should().NotContain(task);
    }

    // trhow exception when task not found
    [Fact(DisplayName = nameof(DeleteTaskUseCase_ThrowExceptionWhenTaskNotFound))]
    [Trait("Application", "Task - Entity")]
    public async void DeleteTaskUseCase_ThrowExceptionWhenTaskNotFound()
    {
        var (mock, user) = _fixture.GetUserRepositoryMockWithUser();
        var userTask = _fixture.CreateValidTaskUser(user.Id);
        var (repositoryMock, task) = _fixture.GetTaskRepositoryMockWithTask(userTask);

        var useCase = new DeleteTask(repositoryMock.Object, mock.Object);
        var input = new DeleteTaskInput(Guid.NewGuid());

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
    }

    // trhow exception when user not found
    [Fact(DisplayName = nameof(DeleteTaskUseCase_ThrowExceptionWhenUserNotFound))]
    [Trait("Application", "Task - Entity")]
    public async void DeleteTaskUseCase_ThrowExceptionWhenUserNotFound()
    {
        var mock = _fixture.GetUserRepositoryMock();
        var (repositoryMock, task) = _fixture.GetTaskRepositoryMockWithTask();


        var useCase = new DeleteTask(repositoryMock.Object, mock.Object);
        var input = new DeleteTaskInput(task.Id);

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}
