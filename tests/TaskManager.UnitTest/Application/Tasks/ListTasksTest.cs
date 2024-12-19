using TaskManager.Application.UseCases.TaskUser.ListTasks;
using TaskManager.Domain.Enums;

namespace TaskManager.UnitTest.Application.Tasks;

[Collection(nameof(TaskFixtureApplicationCollection))]
public class ListTasksTest
{
    private readonly TaskFixtureApplication _fixture;

    public ListTasksTest(TaskFixtureApplication fixture)
    {
        _fixture = fixture;
    }


    // get all tasks without filter
    [Fact(DisplayName = nameof(ListTasksUseCase))]
    [Trait("Application", "Task - Entity")]
    public async Task ListTasksUseCase()
    {
        var (mock, user) = _fixture.GetUserRepositoryMockWithUser();
        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMocForFilter();

        var useCase = new ListTasks(mockTaskRepository.Object, mock.Object);
        var output = await useCase.Handle(new ListTasksInput(), CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().HaveCount(10);

    }

    // get all tasks with filter by category personal
    [Fact(DisplayName = nameof(ListTasksUseCaseWithCategoryFilter))]
    [Trait("Application", "Task - Entity")]
    public async Task ListTasksUseCaseWithCategoryFilter()
    {
        var category = CategoryEnuns.Personal;
        var (mock, _) = _fixture.GetUserRepositoryMockWithUser();
        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMocForFilter(category: category);
        var input = new ListTasksInput(category: category);
        var useCase = new ListTasks(mockTaskRepository.Object, mock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().Contain(x => x.Category == CategoryEnuns.Personal);
    }

    // get all tasks with filter by category work
    [Fact(DisplayName = nameof(ListTasksUseCaseWithCategoryFilterWork))]
    [Trait("Application", "Task - Entity")]
    public async Task ListTasksUseCaseWithCategoryFilterWork()
    {
        var category = CategoryEnuns.Work;
        var (mock, _) = _fixture.GetUserRepositoryMockWithUser();
        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMocForFilter(category: category);
        var input = new ListTasksInput(category: category);
        var useCase = new ListTasks(mockTaskRepository.Object, mock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().HaveCount(10);
    }

    // get all tasks with filter by category study
    [Fact(DisplayName = nameof(ListTasksUseCaseWithCategoryFilterStudy))]
    [Trait("Application", "Task - Entity")]
    public async Task ListTasksUseCaseWithCategoryFilterStudy()
    {
        var category = CategoryEnuns.Study;
        var (mock, _) = _fixture.GetUserRepositoryMockWithUser();
        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMocForFilter(category: category);
        var input = new ListTasksInput(category: category);
        var useCase = new ListTasks(mockTaskRepository.Object, mock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().HaveCount(10);
    }

    // get all tasks with filter by category others
    [Fact(DisplayName = nameof(ListTasksUseCaseWithCategoryFilterOthers))]
    [Trait("Application", "Task - Entity")]
    public async Task ListTasksUseCaseWithCategoryFilterOthers()
    {
        var category = CategoryEnuns.Others;
        var (mock, _) = _fixture.GetUserRepositoryMockWithUser();
        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMocForFilter(category: category);
        var input = new ListTasksInput(category: category);
        var useCase = new ListTasks(mockTaskRepository.Object, mock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().HaveCount(10);
    }

    // get all tasks with filter by user id
    [Fact(DisplayName = nameof(ListTasksUseCaseWithUserIdFilter))]
    [Trait("Application", "Task - Entity")]
    public async Task ListTasksUseCaseWithUserIdFilter()
    {
        var (mock, user) = _fixture.GetUserRepositoryMockWithUser();
        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMocForFilter(user: user);
        var input = new ListTasksInput(userId: user.Id);
        var useCase = new ListTasks(mockTaskRepository.Object, mock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().HaveCount(10);
    }

    // get all tasks with filter by user name
    [Fact(DisplayName = nameof(ListTasksUseCaseWithUserNameFilter))]
    [Trait("Application", "Task - Entity")]
    public async Task ListTasksUseCaseWithUserNameFilter()
    {
        var user = _fixture.GetValidUser();
        var userRepositoryMock = _fixture.GetUserRepositoryMock();

        userRepositoryMock.Setup(x => x.GetByUserName(user.UserName))
            .ReturnsAsync(user);

        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMocForFilter(user: user);
        var input = new ListTasksInput(userName: user.UserName);
        var useCase = new ListTasks(mockTaskRepository.Object, userRepositoryMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Should().HaveCount(10);
    }

    // get all tasks with filter by user name and category
    [Fact(DisplayName = nameof(ListTasksUseCaseWithUserNameAndCategoryFilter))]
    [Trait("Application", "Task - Entity")]
    public async Task ListTasksUseCaseWithUserNameAndCategoryFilter()
    {
        var user = _fixture.GetValidUser();
        var userRepositoryMock = _fixture.GetUserRepositoryMock();

        userRepositoryMock.Setup(x => x.GetByUserName(user.UserName))
            .ReturnsAsync(user);

        var (mockTaskRepository, _) = _fixture.GetTaskRepositoryMocForFilter(user: user);



        var input = new ListTasksInput(userName: user.UserName, category: CategoryEnuns.Personal);
        var useCase = new ListTasks(mockTaskRepository.Object, userRepositoryMock.Object);
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();

    }
}
