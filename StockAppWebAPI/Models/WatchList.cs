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
        public int UserId { get; set; }
        [Key]
        [ForeignKey("Stock")]
        public int StockId { get; set; }

        // Liên kết với bảng Users
        public User? User { get; set; }

        // Liên kết với bảng Stocks (bạn cần tạo model cho Stocks nếu chưa có)
        public Stock? Stock { get; set; }
    }
}
