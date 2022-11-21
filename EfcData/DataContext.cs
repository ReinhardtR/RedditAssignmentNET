using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcData;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcData/ReadIt.db");
    }
}