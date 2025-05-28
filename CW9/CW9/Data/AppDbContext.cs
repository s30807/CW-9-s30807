using CW9.Models;
using Microsoft.EntityFrameworkCore;

namespace CW9.Data;

public class AppDbContext : DbContext
{
    public DbSet<Doctor> Students { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}