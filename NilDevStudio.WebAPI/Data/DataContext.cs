using Microsoft.EntityFrameworkCore;
using NilDevStudio.WebAPI.Models;

namespace NilDevStudio.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DbSet<MyEvent> MyEvents { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}