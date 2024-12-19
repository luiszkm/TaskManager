using TaskManager.Application.UseCases.User.Create;
using TaskManager.Application.UseCases.User.Update;
using TaskManager.Domain.Repositories;
using TaskManager.UnitTest.Common;

namespace TaskManager.UnitTest.Application.User;

[CollectionDefinition(nameof(UserFixtureApplicationCollection))]
public class UserFixtureApplicationCollection : ICollectionFixture<UserFixtureApplication>
{

}

public class UserFixtureApplication : UserFixture
{
    public CreateUserInput CreateUserInput() =>
        new(UserName, Password);

    public Mock<IUserRepository> GetUserRepositoryMock()
      => new();

    public UpdateUserInput UpdateUserInput(Guid id) =>
        new(id, Password);

    public DomainEntity.User UserExample() =>
        new(UserName, Password);


    // get mock with user
    public Mock<IUserRepository> GetUserRepositoryMockWithUser(DomainEntity.User user)
    {
        var mock = GetUserRepositoryMock();
        mock.Setup(x => x.GetById(user.Id))
            .ReturnsAsync(user);
        return mock;
    }
}
