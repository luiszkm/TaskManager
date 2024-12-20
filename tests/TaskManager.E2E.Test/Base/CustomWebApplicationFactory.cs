using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infra.EFCore;

namespace TaskManager.E2E.Test.Base;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // InMemory
        builder.ConfigureServices(service =>
        {
            var dboptions = service.FirstOrDefault(
                x => x.ServiceType ==
                     typeof(DbContextOptions<TaskManegDbContext>));
            if (dboptions != null)
                service.Remove(dboptions);

            service.AddDbContext<TaskManegDbContext>(
                options => options.UseInMemoryDatabase("Db-In-Memory"));
        });
        base.ConfigureWebHost(builder);
    }
}
