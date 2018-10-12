using Microsoft.EntityFrameworkCore;
using pindogramApp.Entities;


namespace pindogramApp.Entities
{
    public class PindogramDataContext : DbContext
    {
        public PindogramDataContext(DbContextOptions<PindogramDataContext> options) : base(options)
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<pindogramApp.Entities.Meme> Meme { get; set; }
    }
}
