using API_asp_start_project.Domain.Interfaces;
using API_asp_start_project.Infrastructure.Repositories;
using API_asp_start_project.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace API_asp_start_project.Config
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyOrigin().AllowAnyHeader());
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["mysqlconnection:connectionString"];

            services.AddDbContext<RepositoryContext>(option => option.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services )
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
