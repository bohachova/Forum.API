using Microsoft.EntityFrameworkCore;
using Forum.API.DataObjects.UserObjects;

namespace Forum.API.DAL
{
    public class ForumDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
