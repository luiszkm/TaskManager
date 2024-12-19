using TaskManager.Application.UseCases.TaskUser.ChangeStatus;

namespace TaskManager.UnitTest.Application.Tasks;

[Collection(nameof(TaskFixtureApplicationCollection))]
public class ChangeStatusTaskTest
{
    private readonly TaskFixtureApplication _fixture;

    public ChangeStatusTaskTest(TaskFixtureApplication fixture)
    {
        _fixture = fixture;
    }

    // complete task
    [Fact(DisplayName = nameof(CompleteTaskUseCase))]
    [Trait("Application", "Task - Entity")]
    public async void CompleteTaskUseCase()
    {
        var userTask = _fixture.CreateValidTaskUser();
        var (repositoryMock, task) = _fixture.GetTaskRepositoryMockWithTask(userTask);

        var useCase = new ChangeStatusTask(repositoryMock.Object);
        var input = new ChangeStatusInput(task.Id, true);

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.GetById(task.Id), Times.Once);
        repositoryMock.Verify(x => x.Update(task), Times.Once);


        task.IsCompleted.Should().BeTrue();
    }

    // uncomplete task
    [Fact(DisplayName = nameof(UncompleteTaskUseCase))]
    [Trait("Application", "Task - Entity")]
    public async void UncompleteTaskUseCase()
    {
        var userTask = _fixture.CreateValidTaskUser();
        var (repositoryMock, task) = _fixture.GetTaskRepositoryMockWithTask(userTask);

        var useCase = new ChangeStatusTask(repositoryMock.Object);
        var input = new ChangeStatusInput(task.Id, false);

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.GetById(task.Id), Times.Once);
        repositoryMock.Verify(x => x.Update(task), Times.Once);

        task.IsCompleted.Should().BeFalse();
    }
}
