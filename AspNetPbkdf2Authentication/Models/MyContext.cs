using Microsoft.EntityFrameworkCore;

namespace AspNetPbkdf2Authentication.Models
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseMySQL("server=192.168.0.200;database=DbAuthDemo;user=pilnyjakub;password=123456");
        }
    }
}
