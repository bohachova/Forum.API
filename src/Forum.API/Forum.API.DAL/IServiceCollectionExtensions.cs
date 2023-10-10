using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Forum.API.DAL
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddForumApiDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<ForumDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("main"));
            });

            return services;
        }
    }
}
