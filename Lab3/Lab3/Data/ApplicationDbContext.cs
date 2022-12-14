using Lab3.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Data;

public class ApplicationDbContext : DbContext
{

    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    public DbSet<NodeValue> NodeValues { get; set; }
}