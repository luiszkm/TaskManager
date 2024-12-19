using TaskManager.Domain.Enums;
using TaskManager.Domain.Repositories;

namespace TaskManager.UnitTest.Common;


public class BaseFixture
{
    public Faker Faker { get; private set; }
    public BaseFixture() => Faker = new Faker();
    public string GetTitle() => Faker.Name.JobTitle();
    public string GetDescription() => Faker.Lorem.Paragraph();

    public CategoryEnuns GetCategory() => Faker.PickRandom<CategoryEnuns>();

    public Mock<IUserRepository> GetUserRepositoryMock()
      => new();

    public Mock<ITasksRepository> GetTaskRepositoryMock()
        => new();

    public DomainEntity.TaskUser CreateValidTaskUser(Guid? userId = null, CategoryEnuns? category = CategoryEnuns.Others) =>
        new(
             GetTitle(),
             GetDescription(),
             category ?? GetCategory(),
             userId ?? Guid.NewGuid()
            );


}
