using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using Repositories.Contracts;
using Services.Contracts;
using Services;

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
        public static void ConfigureServiceManager(this IServiceCollection service)
        {
            service.AddScoped<IServiceManager, ServiceManager>();
        }
        public static void ConfigureLoggerService(this IServiceCollection service)
        {
            service.AddSingleton<ILoggerService,LoggerManager>();
        }
    }
}
