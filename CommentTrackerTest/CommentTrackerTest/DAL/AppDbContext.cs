using CommentTrackerTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CommentTrackerTest.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {
                
        }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
