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
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<RealtimeQuote> RealtimeQuotes { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CoveredWarrant> CoveredWarrants { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //code này cho biết gộp 2 thà này sẽ tạo khóa chính của bảng WatchList
            //tức là nếu có 2 trường UserId và StockId giống hệt nhau thì lúc đó sẽ không cho insert nữa 
            modelBuilder.Entity<WatchList>()
                .HasKey(w=>new {w.UserId, w.StockId});
            modelBuilder.Entity<Order>()
                .ToTable(table => table.HasTrigger("trigger_orders"));
        }
    }
}
