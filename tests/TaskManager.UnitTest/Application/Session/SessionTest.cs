using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.Session;

namespace TaskManager.UnitTest.Application.Session;

[Collection(nameof(SessionFixtureCollection))]
public class SessionTest
{
    private readonly SessionFixture _fixture;

    public SessionTest(SessionFixture fixture)
    {
        _fixture = fixture;
    }


    // succes login
    [Fact(DisplayName = nameof(SuccesLogin))]
    [Trait("Application", "Session")]
    public async Task SuccesLogin()
    {
        var userName = _fixture.GetUserName();
        var password = _fixture.GetPassword();
        var user = _fixture.GetValidUser(userName, password);
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthRepositoryMock();
        repositoryMock.Setup(u => u.GetByUserName(It.IsAny<string>()))
          .ReturnsAsync((string email) => user);

        var useCase = new Sessions(authMock.Object, repositoryMock.Object);

        var input = new SessionInput(userName, password);

        var output = await useCase.Handle(input, CancellationToken.None);

        authMock.Verify(
         r => r.GenerateToken(
             It.IsAny<Guid>()),
         Times.Once);


        output.Should().NotBeNull();
        output.Token.Should().NotBeEmpty();
        output.UserId.Should().NotBeEmpty();
        output.UserId.Should().Be(user.Id);

    }

    //throw exception when user not found
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNotFound))]
    [Trait("Application", "Session")]
    public async Task ThrowExceptionWhenUserNotFound()
    {
        var userName = _fixture.GetUserName();
        var password = _fixture.GetPassword();
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthRepositoryMock();
        repositoryMock.Setup(u => u.GetByUserName(It.IsAny<string>()))
          .ReturnsAsync((DomainEntity.User)null);

        var useCase = new Sessions(authMock.Object, repositoryMock.Object);

        var input = new SessionInput(userName, password);

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<ApplicationUnauthorizedException>();

    }

    // throw exception when password is invalid
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsInvalid))]
    [Trait("Application", "Session")]
    public async Task ThrowExceptionWhenPasswordIsInvalid()
    {
        var userName = _fixture.GetUserName();
        var password = _fixture.GetPassword();
        var user = _fixture.GetValidUser(userName, password);
        var repositoryMock = _fixture.GetUserRepositoryMock();
        var authMock = _fixture.GetAuthRepositoryMock();
        repositoryMock.Setup(u => u.GetByUserName(It.IsAny<string>()))
          .ReturnsAsync((string email) => user);

        var useCase = new Sessions(authMock.Object, repositoryMock.Object);

        var input = new SessionInput(userName, _fixture.GetPassword());

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<ApplicationUnauthorizedException>();

    }


}
