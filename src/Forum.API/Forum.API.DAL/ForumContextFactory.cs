using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Forum.API.DAL
{
    public class ForumContextFactory : IDesignTimeDbContextFactory<ForumDbContext>
    {
        private readonly IConfiguration configuration;
        public ForumContextFactory() {}
        public ForumContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public ForumDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ForumDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("main"));
            return new ForumDbContext(builder.Options);
        }
    }
}
