using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using Repositories.Contracts;
namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection service,IConfiguration configuration)
        {
            service
                .AddDbContext<RepositoryContext>(
                opt => opt.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        }
        public static void ConfigureRepositoryManager(this IServiceCollection service)
        {
            service.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
