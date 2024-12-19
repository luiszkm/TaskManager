using TaskManager.Application.UseCases.User.Update;
using TaskManager.Domain.Exceptions;

namespace TaskManager.UnitTest.Application.User;

[Collection(nameof(UserFixtureApplicationCollection))]
public class UpdateUserTest
{

    private readonly UserFixtureApplication _fixture;

    public UpdateUserTest(UserFixtureApplication fixture)
    {
        _fixture = fixture;
    }

    // update password
    [Fact(DisplayName = nameof(UpdateUserPassword))]
    public async Task UpdateUserPassword()
    {
        var userExample = _fixture.UserExample();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(userExample);

        var useCase = new UpdateUser(repositoryMock.Object);
        var input = _fixture.UpdateUserInput(userExample.Id);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.ChangePassword(It.IsAny<DomainEntity.User>()), Times.Once);

        output.Should().NotBeNull();
        output.Id.Should().Be(userExample.Id);
        output.UserName.Should().Be(userExample.UserName);
        output.Tasks.Should().BeEmpty();
        output.CreatedAt.Should().Be(userExample.CreatedAt);

    }

    // throw when  password is null
    [Fact(DisplayName = nameof(UpdateUserPasswordThrowWhenPasswordIsNull))]
    public async Task UpdateUserPasswordThrowWhenPasswordIsNull()
    {
        var userExample = _fixture.UserExample();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(userExample);

        var useCase = new UpdateUser(repositoryMock.Object);
        var input = _fixture.UpdateUserInput(userExample.Id);
        input.Password = null;

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();

    }

    // throw when  password is empty
    [Fact(DisplayName = nameof(UpdateUserPasswordThrowWhenPasswordIsEmpty))]
    public async Task UpdateUserPasswordThrowWhenPasswordIsEmpty()
    {
        var userExample = _fixture.UserExample();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(userExample);

        var useCase = new UpdateUser(repositoryMock.Object);
        var input = _fixture.UpdateUserInput(userExample.Id);
        input.Password = string.Empty;

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();

    }

    // throw when  password is whitespace
    [Fact(DisplayName = nameof(UpdateUserPasswordThrowWhenPasswordIsWhiteSpace))]
    public async Task UpdateUserPasswordThrowWhenPasswordIsWhiteSpace()
    {
        var userExample = _fixture.UserExample();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(userExample);

        var useCase = new UpdateUser(repositoryMock.Object);
        var input = _fixture.UpdateUserInput(userExample.Id);
        input.Password = " ";

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();

    }

    // throw when  password is less than 3 characters
    [Fact(DisplayName = nameof(ThrowWhenPasswordIsLessThan3Characters))]
    public async Task ThrowWhenPasswordIsLessThan3Characters()
    {
        var userExample = _fixture.UserExample();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(userExample);

        var useCase = new UpdateUser(repositoryMock.Object);
        var input = _fixture.UpdateUserInput(userExample.Id);
        input.Password = "12";

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();

    }

    // throw when  password is greater than 50 characters
    [Fact(DisplayName = nameof(ThrowWhenPasswordIsGreaterThan50Characters))]
    public async Task ThrowWhenPasswordIsGreaterThan50Characters()
    {
        var userExample = _fixture.UserExample();
        var repositoryMock = _fixture.GetUserRepositoryMockWithUser(userExample);

        var useCase = new UpdateUser(repositoryMock.Object);
        var input = _fixture.UpdateUserInput(userExample.Id);
        input.Password = new string('a', 51);

        Func<Task> act = async () => await useCase.Handle(input, CancellationToken.None);

        await act.Should().ThrowAsync<EntityValidationException>();

    }



}
