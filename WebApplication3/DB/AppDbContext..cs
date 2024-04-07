using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
namespace WebApplication3.DB
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
    }
}
