using TaskManager.Application.UseCases.User.Create;
using TaskManager.Domain.Exceptions;

namespace TaskManager.UnitTest.Application.User;

[Collection(nameof(UserFixtureApplicationCollection))]
public class CreateUserTest
{
    private readonly UserFixtureApplication _fixture;

    public CreateUserTest(UserFixtureApplication fixture)
    {
        _fixture = fixture;
    }


    // create user with valid data
    [Fact(DisplayName = nameof(CreateUserWithValidData))]
    public async Task CreateUserWithValidData()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthorizationMock();
        var useCase = new CreateUser(repositoryMock.Object, authMock.Object);
        var input = _fixture.CreateUserInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.Create(It.IsAny<DomainEntity.User>()), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.UserName.Should().Be(input.UserName);
        output.Tasks.Should().BeEmpty();
        output.CreatedAt.Should().BeBefore(DateTime.UtcNow.AddSeconds(10));

    }

    // throw when cant instantiate user with user name null
    [Fact(DisplayName = nameof(ThrowWhenCantInstantiateUser))]
    public async Task ThrowWhenCantInstantiateUser()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthorizationMock();
        var useCase = new CreateUser(repositoryMock.Object, authMock.Object);
        var input = _fixture.CreateUserInput();
        input.UserName = null;

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();
    }

    // throw when cant instantiate user with user name empty
    [Fact(DisplayName = nameof(ThrowWhenCantInstantiateUserWithUserNmaeEmpty))]
    public async Task ThrowWhenCantInstantiateUserWithUserNmaeEmpty()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthorizationMock();

        var useCase = new CreateUser(repositoryMock.Object, authMock.Object);

        var input = _fixture.CreateUserInput();
        input.UserName = string.Empty;

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();
    }

    // throw when cant instantiate user with user name whitespace
    [Fact(DisplayName = nameof(ThrowWhenCantInstantiateUserWithUserNameWhiteSpace))]
    public async Task ThrowWhenCantInstantiateUserWithUserNameWhiteSpace()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthorizationMock();
        var useCase = new CreateUser(repositoryMock.Object, authMock.Object);
        var input = _fixture.CreateUserInput();
        input.UserName = " ";

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();
    }

    // throw when cant instantiate user with password whitespace
    [Fact(DisplayName = nameof(ThrowWhenCantInstantiateUserWithPasswordWhiteSpace))]
    public async Task ThrowWhenCantInstantiateUserWithPasswordWhiteSpace()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthorizationMock();
        var useCase = new CreateUser(repositoryMock.Object, authMock.Object);
        var input = _fixture.CreateUserInput();
        input.Password = " ";

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<ApplicationException>();
    }

    // throw when cant instantiate user with password null
    [Fact(DisplayName = nameof(ThrowWhenCantInstantiateUserWithPasswordNull))]
    public async Task ThrowWhenCantInstantiateUserWithPasswordNull()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthorizationMock();
        var useCase = new CreateUser(repositoryMock.Object, authMock.Object);
        var input = _fixture.CreateUserInput();
        input.Password = null;

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<ApplicationException>();
    }

    // throw when cant instantiate user with password empty
    [Fact(DisplayName = nameof(ThrowWhenCantInstantiateUserWithPasswordEmpty))]
    public async Task ThrowWhenCantInstantiateUserWithPasswordEmpty()
    {
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthorizationMock();
        var useCase = new CreateUser(repositoryMock.Object, authMock.Object);
        var input = _fixture.CreateUserInput();
        input.Password = string.Empty;

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<ApplicationException>();
    }

}
