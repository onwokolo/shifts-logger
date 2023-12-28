// AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using ShiftLoggerApi.Models;

namespace ShiftLoggerApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Shift> Shifts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
