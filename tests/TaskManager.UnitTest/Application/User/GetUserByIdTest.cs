using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.User.GetById;

namespace TaskManager.UnitTest.Application.User;

[Collection(nameof(UserFixtureApplicationCollection))]
public class GetUserByIdTest
{
    private readonly UserFixtureApplication _fixture;

    public GetUserByIdTest(UserFixtureApplication fixture)
    {
        _fixture = fixture;
    }


    // get user by id with valid data
    [Fact(DisplayName = nameof(GetUserByIdWithValidData))]
    [Trait("User", "User - Application")]
    public async Task GetUserByIdWithValidData()
    {
        var user = _fixture.UserExample();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(user);
        var useCase = new GetUserById(repositoryMock.Object);
        var input = new GetUserByIdInput(user.Id);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Tasks.Should().BeEmpty();
        output.CreatedAt.Should().BeBefore(DateTime.UtcNow.AddSeconds(10));

    }

    // throw when user not found
    [Fact(DisplayName = nameof(ThrowWhenUserNotFound))]
    [Trait("User", "User - Application")]
    public async Task ThrowWhenUserNotFound()
    {
        var user = _fixture.UserExample();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(user);
        var useCase = new GetUserById(repositoryMock.Object);
        var input = new GetUserByIdInput(Guid.NewGuid());

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();
    }
}
