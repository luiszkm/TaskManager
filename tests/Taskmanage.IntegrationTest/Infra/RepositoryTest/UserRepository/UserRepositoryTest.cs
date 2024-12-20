
namespace Taskmanage.IntegrationTest.Infra.RepositoryTest.UserRepositoryTest;

[Collection(nameof(UserRepositoryTestFixtureCollection))]
public class UserRepositoryTest
{
    private readonly UserRepositoryTestFixture _fixture;

    public UserRepositoryTest(UserRepositoryTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(InsertUser))]
    [Trait("Integration", "User - repositoryl")]
    public async Task InsertUser()
    {
        var user = _fixture.CreateValidUser();
        var dbContext = _fixture.GetDbContext();
        var repository = new Repository.UserRepository(dbContext);

        await repository.Create(user);

        dbContext.Users.Should().Contain(user);

    }

    // throw when user exists
    [Fact(DisplayName = nameof(ThrowWhenUserExists))]
    [Trait("Integration", "User - repository")]
    public async Task ThrowWhenUserExists()
    {
        var user = _fixture.CreateValidUser();
        var dbContext = _fixture.GetDbContext();
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var repository = new Repository.UserRepository(dbContext);

        Func<Task> act = async () => await repository.Create(user);

        await act.Should().ThrowAsync<Exception>();
    }

    // Get user ById
    [Fact(DisplayName = nameof(GetUserById))]
    [Trait("Integration", "User - repository")]
    public async Task GetUserById()
    {
        // Arrange
        var user = _fixture.CreateValidUser();
        var dbContext = _fixture.GetDbContext(true);

        // Insere o usuário no banco
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();

        var repository = new Repository.UserRepository(dbContext);

        // Act
        var userDb = await repository.GetById(user.Id);

        // Assert
        userDb.Should().NotBeNull();
        userDb.Id.Should().Be(user.Id);
        userDb.UserName.Should().Be(user.UserName);
    }

    // throw when user not found
    [Fact(DisplayName = nameof(ThrowWhenUserNotFound))]
    [Trait("Integration", "User - repository")]
    public async Task ThrowWhenUserNotFound()
    {
        var userId = Guid.NewGuid();
        var dbContext = _fixture.GetDbContext();
        var repository = new Repository.UserRepository(dbContext);

        Func<Task> act = async () => await repository.GetById(userId);

        await act.Should().ThrowAsync<Exception>();
    }

}
