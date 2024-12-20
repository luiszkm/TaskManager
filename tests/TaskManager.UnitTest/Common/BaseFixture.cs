using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.UnitTest.Common;


public class BaseFixture
{
    public string UserName => Faker.Person.UserName;
    public string Password => Faker.Internet.Password();
    public Faker Faker { get; private set; }
    public BaseFixture() => Faker = new Faker();
    public string GetTitle() => Faker.Name.JobTitle();
    public string GetDescription() => Faker.Lorem.Paragraph();
    public DomainEntity.User CreateValidUser() => new(UserName, Password);

    public Guid GetGuid() => Guid.NewGuid();

    public CategoryEnuns GetCategory() => Faker.PickRandom<CategoryEnuns>();

    public Mock<IUserRepository> GetUserRepositoryMock()
      => new();

    public Mock<ITasksRepository> GetTaskRepositoryMock()
        => new();

}
