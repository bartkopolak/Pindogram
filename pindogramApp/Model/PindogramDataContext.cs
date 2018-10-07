using Microsoft.EntityFrameworkCore;


namespace pindogramApp.Model
{
    public class PindogramDataContext : DbContext
    {
        public PindogramDataContext(DbContextOptions<PindogramDataContext> options) : base(options)
        {

        }

        public DbSet<Group> Groups { get; set; }
    }
}
