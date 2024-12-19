namespace TaskManager.UnitTest.Common;


[CollectionDefinition(nameof(TaskUserFixtureCollection))]
public class TaskUserFixtureCollection : ICollectionFixture<TaskUserFixture>
{

}
public class TaskUserFixture : BaseFixture
{
    public string UserName => Faker.Person.UserName;
    public string Password => Faker.Internet.Password();
    public DomainEntity.User GetValidUser(string? userName = null, string? password = null)
        => new(
            userName ?? UserName,
            password ?? Password
            );

}
