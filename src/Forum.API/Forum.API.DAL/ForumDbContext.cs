using Microsoft.EntityFrameworkCore;
using Forum.API.DataObjects.UserObjects;
using Forum.API.DataObjects.TopicObjects;

namespace Forum.API.DAL
{
    public class ForumDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }
    }
}
