
using TaskManager.Domain.Exceptions;
using TaskManager.UnitTest.Common;


namespace TaskManager.UnitTest.Domain.Entity;

[Collection(nameof(UserFixtureCollection))]
public class UserTest
{

    private readonly UserFixture _fixture;

    public UserTest(UserFixture fixture)
    {
        _fixture = fixture;
    }

    // instanteate user
    [Fact(DisplayName = nameof(InstantiateUser))]
    public void InstantiateUser()
    {
        var userName = _fixture.UserName;
        var password = _fixture.Password;
        DateTime dateTime = DateTime.Now.AddMinutes(5);

        var user = new DomainEntity.User(userName, password);

        user.UserName.Should().Be(userName);
        user.Password.Should().Be(password);
        user.Tasks.Should().BeEmpty();
        user.Id.Should().NotBeEmpty();
        user.CreatedAt.Should().BeBefore(DateTime.UtcNow);
    }

    // throw exception when user name is null
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsNull))]
    public void ThrowExceptionWhenUserNameIsNull()
    {
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(null, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is null
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsNull))]
    public void ThrowExceptionWhenPasswordIsNull()
    {
        var userName = _fixture.UserName;

        Action act = () => new DomainEntity.User(userName, null);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when user name is empty
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsEmpty))]
    public void ThrowExceptionWhenUserNameIsEmpty()
    {
        var userName = string.Empty;
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is empty
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsEmpty))]
    public void ThrowExceptionWhenPasswordIsEmpty()
    {
        var userName = _fixture.UserName;
        var password = string.Empty;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when user name is less than 3 characters
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsLessThan3Characters))]
    public void ThrowExceptionWhenUserNameIsLessThan3Characters()
    {
        var userName = "us";
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is less than 3 characters
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsLessThan3Characters))]
    public void ThrowExceptionWhenPasswordIsLessThan3Characters()
    {
        var userName = _fixture.UserName;
        var password = "pa";

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when user name is greater than 50 characters
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsGreaterThan50Characters))]
    public void ThrowExceptionWhenUserNameIsGreaterThan50Characters()
    {
        var userName = new string('a', 51);
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is greater than 50 characters
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsGreaterThan50Characters))]
    public void ThrowExceptionWhenPasswordIsGreaterThan50Characters()
    {
        var userName = _fixture.UserName;
        var password = new string('a', 51);

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when user name is whitespace
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsWhitespace))]
    public void ThrowExceptionWhenUserNameIsWhitespace()
    {
        var userName = " ";
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is whitespace
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsWhitespace))]
    public void ThrowExceptionWhenPasswordIsWhitespace()
    {
        var userName = _fixture.UserName;
        var password = " ";

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // add task to user
    [Fact(DisplayName = nameof(AddTaskToUser))]
    public void AddTaskToUser()
    {
        var user = _fixture.CreateValidUser();
        var taskId = Guid.NewGuid();

        user.AddTask(taskId);

        user.Tasks.Should().Contain(taskId);
    }

    // remove task from user
    [Fact(DisplayName = nameof(RemoveTaskFromUser))]
    public void RemoveTaskFromUser()
    {
        var user = _fixture.CreateValidUser();
        var taskId = Guid.NewGuid();

        user.AddTask(taskId);
        user.RemoveTask(taskId);

        user.Tasks.Should().NotContain(taskId);
    }

    // change password
    [Fact(DisplayName = nameof(ChangePassword))]
    public void ChangePassword()
    {
        var user = _fixture.CreateValidUser();
        var newPassword = _fixture.Password;

        user.ChangePassword(newPassword);

        user.Password.Should().Be(newPassword);
    }

}
