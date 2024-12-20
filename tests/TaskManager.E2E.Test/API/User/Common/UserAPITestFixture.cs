using TaskManager.Application.UseCases.User.Create;
using TaskManager.E2E.Test.Base;

namespace TaskManager.E2E.Test.API.User.Common;

[CollectionDefinition(nameof(UserAPITestFixtureCollection))]
public class UserAPITestFixtureCollection : ICollectionFixture<UserAPITestFixture>
{
}

public class UserAPITestFixture : BaseFixture
{
    public UserPersistence Persistence;

    public UserAPITestFixture() : base()
    {
        Persistence = new UserPersistence(GetDbContextInMemory(true));
    }

    public string GetUserName() => Faker.Internet.UserName();
    public string GetPassword() => Faker.Internet.Password();
    public string GetTitle() => Faker.Internet.Random.AlphaNumeric(10);
    public string GetValidDescription()
    => Faker.Lorem.Paragraph();

    public DomainEntity.User CreateValidUserWithoutPassword(
        string password
        )
         => new(GetUserName(), password);

    public DomainEntity.User CreateValidUser()
    => new(GetUserName(), GetPassword());
    public DomainEntity.User GeteValidUser(
       )
        => new(GetUserName(),
            GetPassword()

            );

    public CreateUserInput CreateValidUserInput()
        => new(
            GetUserName(),
            GetPassword()
            );








}
