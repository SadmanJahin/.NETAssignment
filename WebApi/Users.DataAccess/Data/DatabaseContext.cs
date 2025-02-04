using Microsoft.EntityFrameworkCore;
using Users.Core.Entities;


namespace Users.DataAccess.Data
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}
