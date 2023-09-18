using Microsoft.EntityFrameworkCore;
using StockAppWebApi.Models;

namespace StockAppWebAPI.Models
{
    public class StockAppContext: DbContext
    {
        public StockAppContext(DbContextOptions<StockAppContext> options) 
            : base(options) 
        {
        
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
