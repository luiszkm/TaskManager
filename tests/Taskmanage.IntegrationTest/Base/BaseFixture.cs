using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Enums;
using TaskManager.Infra.EFCore;

namespace Taskmanage.IntegrationTest.Base;

public class BaseFixture

{

    private readonly TaskManegDbContext dbContext;
    public Faker Faker { get; set; }
    public BaseFixture()
       => Faker = new Faker("pt_BR");
    public string UserName => Faker.Person.UserName;
    public string Password => Faker.Internet.Password();

    public string GetTitle() => Faker.Name.JobTitle();
    public string GetDescription() => Faker.Lorem.Paragraph();
    public CategoryEnuns GetCategory() => Faker.PickRandom<CategoryEnuns>();

    public DomainEntity.User CreateValidUser() => new(UserName, Password);

    public DomainEntity.TaskUser CreateValidTaskUser(DomainEntity.User user, CategoryEnuns? category = null) =>
     new(
          GetTitle(),
          GetDescription(),
          category ?? CategoryEnuns.Personal,
          user.Id
         );

    public DomainEntity.TaskUser CreateValidTaskUserForFilter(DomainEntity.User user, CategoryEnuns category) =>
   new(
        GetTitle(),
        GetDescription(),
        category,
        user.Id
       );

    public TaskManegDbContext GetDbContext(bool preserverData = false)
    {
        var context = new TaskManegDbContext(
     new DbContextOptionsBuilder<TaskManegDbContext>()
         .UseInMemoryDatabase(Guid.NewGuid().ToString())
         .Options);



        if (!preserverData)
        {
            context.Database.EnsureCreated();
            context.Database.EnsureDeleted();
        }


        return context;

    }
}
