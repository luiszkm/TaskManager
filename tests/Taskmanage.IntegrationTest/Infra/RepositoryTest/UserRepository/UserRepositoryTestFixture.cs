using Taskmanage.IntegrationTest.Base;

namespace Taskmanage.IntegrationTest.Infra.RepositoryTest.UserRepositoryTest;

[CollectionDefinition(nameof(UserRepositoryTestFixtureCollection))]
public class UserRepositoryTestFixtureCollection : ICollectionFixture<UserRepositoryTestFixture>
{
}

public class UserRepositoryTestFixture : BaseFixture
{
}
