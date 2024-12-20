using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskManager.Infra.EFCore;

namespace TaskManager.E2E.Test.Base;

public class BaseFixture
{
    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
        WebAppFactory = new CustomWebApplicationFactory<Program>();
        HttpClient = WebAppFactory.CreateClient();
        ApiClient = new ApiClient(HttpClient);
        var config = WebAppFactory.Services
            .GetService(typeof(IConfiguration));
        ArgumentNullException.ThrowIfNull(config);
    }
    public ApiClient ApiClient { get; set; }
    public HttpClient HttpClient { get; set; }
    public CustomWebApplicationFactory<Program> WebAppFactory { get; set; }


    public Faker Faker { get; set; }


    public TaskManegDbContext GetDbContextInMemory(bool preserverData = false)
    {
        var context = new TaskManegDbContext(
            new DbContextOptionsBuilder<TaskManegDbContext>()
                .UseInMemoryDatabase("Db-In-Memory")
                .Options);

        if (!preserverData)
            context.Database.EnsureCreated();
        return context;
    }


    public void CleanDatabase(TaskManegDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }



}
