using StockAppWebApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAppWebAPI.Models
{
    [Table("watchlists")]
    public class WatchList
    {
        [Key]
        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        [Key]
        [ForeignKey("Stock")]
        [Column("stock_id")]
        public int StockId { get; set; }

        // Liên kết với bảng Users
        public User? User { get; set; }

        // Liên kết với bảng Stocks (bạn cần tạo model cho Stocks nếu chưa có)
        public Stock? Stock { get; set; }
    }
}
