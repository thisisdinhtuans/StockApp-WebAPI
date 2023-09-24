using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockAppWebAPI.Models
{
[Table("stocks")] // Ánh xạ tên bảng
    public class Stock
    {
        [Key]
        [Column("stock_id")] // Ánh xạ tên cột
        public int StockId { get; set; }

        [Required]
        [MaxLength(10)]
        [Column("symbol")] // Ánh xạ tên cột
        public string Symbol { get; set; } = "";

        [Required]
        [MaxLength(255)]
        [Column("company_name")] // Ánh xạ tên cột
        public string CompanyName { get; set; } = "";

        [Column("market_cap")] // Ánh xạ tên cột
        public decimal? MarketCap { get; set; } // Để cho phép giá trị null

        [MaxLength(200)]
        [Column("sector")] // Ánh xạ tên cột
        public string Sector { get; set; } = "";

        [MaxLength(200)]
        [Column("industry")] // Ánh xạ tên cột
        public string Industry { get; set; } = "";

        [MaxLength(200)]
        [Column("sector_en")] // Ánh xạ tên cột
        public string SectorEn { get; set; } = "";

        [MaxLength(200)]
        [Column("industry_en")] // Ánh xạ tên cột
        public string IndustryEn { get; set; } = "";

        [MaxLength(50)]
        [Column("stock_type")] // Ánh xạ tên cột
        public string StockType { get; set; } = "";

        [Column("rank")] // Ánh xạ tên cột
        public int Rank { get; set; }

        [MaxLength(200)]
        [Column("rank_source")] // Ánh xạ tên cột
        public string RankSource { get; set; } = "";

        [MaxLength(255)]
        [Column("reason")] // Ánh xạ tên cột
        public string Reason { get; set; } = "";
        //navigation
        public ICollection<WatchList>? WatchLists { get; set; }
    }

}
