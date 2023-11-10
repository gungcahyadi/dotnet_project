namespace project3.Helpers;

using Microsoft.EntityFrameworkCore;
using project3.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25)));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Task> Tasks { get; set; }
}