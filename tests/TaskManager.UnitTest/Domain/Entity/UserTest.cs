﻿
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
    [Trait("Domain", "User - Entity")]
    public void InstantiateUser()
    {
        var userName = _fixture.UserName;
        var password = _fixture.Password;

        var user = new DomainEntity.User(userName, password);

        user.UserName.Should().Be(userName);
        user.PasswordHash.Should().Be(password);
        user.Tasks.Should().BeEmpty();
        user.Id.Should().NotBeEmpty();
        user.CreatedAt.Should().BeBefore(DateTime.UtcNow);
    }

    // throw exception when user name is null
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsNull))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenUserNameIsNull()
    {
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(null, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is null
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsNull))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenPasswordIsNull()
    {
        var userName = _fixture.UserName;

        Action act = () => new DomainEntity.User(userName, null);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when user name is empty
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsEmpty))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenUserNameIsEmpty()
    {
        var userName = string.Empty;
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is empty
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsEmpty))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenPasswordIsEmpty()
    {
        var userName = _fixture.UserName;
        var password = string.Empty;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when user name is less than 3 characters
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsLessThan3Characters))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenUserNameIsLessThan3Characters()
    {
        var userName = "us";
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is less than 3 characters
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsLessThan3Characters))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenPasswordIsLessThan3Characters()
    {
        var userName = _fixture.UserName;
        var password = "pa";

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when user name is greater than 50 characters
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsGreaterThan50Characters))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenUserNameIsGreaterThan50Characters()
    {
        var userName = new string('a', 51);
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is greater than 50 characters
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsGreaterThan50Characters))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenPasswordIsGreaterThan50Characters()
    {
        var userName = _fixture.UserName;
        var password = new string('a', 501);

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when user name is whitespace
    [Fact(DisplayName = nameof(ThrowExceptionWhenUserNameIsWhitespace))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenUserNameIsWhitespace()
    {
        var userName = " ";
        var password = _fixture.Password;

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // throw exception when password is whitespace
    [Fact(DisplayName = nameof(ThrowExceptionWhenPasswordIsWhitespace))]
    [Trait("Domain", "User - Entity")]

    public void ThrowExceptionWhenPasswordIsWhitespace()
    {
        var userName = _fixture.UserName;
        var password = " ";

        Action act = () => new DomainEntity.User(userName, password);

        act.Should().Throw<EntityValidationException>();
    }

    // add task to user
    [Fact(DisplayName = nameof(AddTaskToUser))]
    [Trait("Domain", "User - Entity")]

    public void AddTaskToUser()
    {
        var user = _fixture.CreateValidUser();
        var taskId = _fixture.CreateValidTaskUser(user);

        user.AddTask(taskId);

        user.Tasks.Should().Contain(taskId);
        user.UpdatedAt.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));
    }

    // remove task from user
    [Fact(DisplayName = nameof(RemoveTaskFromUser))]
    [Trait("Domain", "User - Entity")]

    public void RemoveTaskFromUser()
    {
        var user = _fixture.CreateValidUser();
        var taskId = _fixture.CreateValidTaskUser(user);

        user.AddTask(taskId);
        user.RemoveTask(taskId);

        user.Tasks.Should().NotContain(taskId);
        user.UpdatedAt.Should().BeAfter(DateTime.UtcNow.AddMinutes(-1));

    }


}
