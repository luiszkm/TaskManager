using TaskManager.Domain.Enums;

namespace TaskManager.UnitTest.Common;



[CollectionDefinition(nameof(UserFixtureCollection))]
public class UserFixtureCollection : ICollectionFixture<UserFixture>
{

}
public class UserFixture : BaseFixture
{

    public DomainEntity.TaskUser CreateValidTaskUser(DomainEntity.User? user = null) =>
       new(
            GetTitle(),
            GetDescription(),
            CategoryEnuns.Others,
            user.Id
           );

}
