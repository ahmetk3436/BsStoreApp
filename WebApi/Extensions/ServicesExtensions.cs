using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using Repositories.Contracts;
using Services.Contracts;
using Services;
using Entities.DTOs;
using Presentation.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Marvin.Cache.Headers;

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
        public static void ConfigureActionFilters(this IServiceCollection service)
        {
           service.AddScoped<ValidationFilterAttribute>();
            service.AddSingleton<LogFilterAttribute>(); 
            service.AddScoped<ValidateMediaTypeAttribute>();
        }
        public static void ConfigureCors(this IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });
        }
        public static void ConfigureDataSharper(this IServiceCollection service)
        {
            service.AddScoped<IDataSharper<BookDto>, DataSharper<BookDto>>();
        }
        public static void AddCustomMediaTypes(this IServiceCollection service)
        {
            service.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config
                .OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if (systemTextJsonOutputFormatter is not null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.btkakademi.hateoas+json");

                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vnd.btkakademi.apiroot+json");
                }

                var xmlOutputFormatter = config
                 .OutputFormatters
                 .OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();
                if (xmlOutputFormatter is not null)
                {
                    xmlOutputFormatter.SupportedMediaTypes
                  .Add("application/vnd.btkakademi.hateoas+xml");
                    xmlOutputFormatter.SupportedMediaTypes
                   .Add("application/vnd.btkakademi.apiroot+xml");
                }
            });
        }
        public static void ConfigureVersioning(this IServiceCollection service)
        {
            service.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }
        public static void ConfigureResponseCaching(this IServiceCollection service)
        {
            service.AddResponseCaching();
        }
        public static void ConfigureHttpCacheHeaders(this IServiceCollection service)
        {
            service.AddHttpCacheHeaders(expirationOpt =>
            {
                expirationOpt.MaxAge = 80;
                expirationOpt.CacheLocation = CacheLocation.Public;
            }, validationOpt =>
            {
                validationOpt.MustRevalidate = false;
            });
        }
    }
}
