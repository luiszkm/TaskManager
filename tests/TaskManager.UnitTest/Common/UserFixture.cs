namespace TaskManager.UnitTest.Common;



[CollectionDefinition(nameof(UserFixtureCollection))]
public class UserFixtureCollection : ICollectionFixture<UserFixture>
{

}
public class UserFixture : BaseFixture
{
    public string UserName => Faker.Person.UserName;
    public string Password => Faker.Internet.Password();
    public DomainEntity.User CreateValidUser() => new(UserName, Password);

}
