using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockAppWebAPI.Models
{
    //tức là cái class RealtimeQuote tương ứng với view_quotes_realtime, tức là khi chúng ta .RealtimeQuote thì nó sẽ hiểu là nó đọc xuống cái view này
    [Table("view_quotes_realtime")]
    //Keyless tức là ở đây nó là cái view nên là nó sẽ không có cái Primary key, sẽ không có khóa chính, cho nên nó nhìn thấy table thì nó sẽ tìm xem khóa chính ở đâu
    [Keyless]
    public class RealtimeQuote
    {
        //tên trường của class đặt theo convention của C# cho nên cuối cùng nó ra như thế này
        [Column("quote_id")] //đây là tên đặt trong bảng<SQL Server>
        public int quoteId { get; set; } // đây là tên đặt trong C#
        [Column("symbol")]
        public string? Symbol { get; set; }

        [Column("company_name")]
        public string? CompanyName { get; set; }

        [Column("index_name")]
        public string? IndexName { get; set; }

        [Column("index_symbol")]
        public string? IndexSymbol { get; set; }

        [Column("market_cap")]
        public decimal MarketCap { get; set; }

        [Column("sector_en")]
        public string? SectorEn { get; set; }

        [Column("industry_en")]
        public string? IndustryEn { get; set; }

        [Column("sector")]
        public string? Sector { get; set; }

        [Column("industry")]
        public string? Industry { get; set; }

        [Column("stock_type")]
        public string? StockType { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("change")]
        public decimal Change { get; set; }

        [Column("percent_change")]
        public decimal PercentChange { get; set; }

        [Column("volume")]
        public int Volume { get; set; }

        [Column("time_stamp")]
        public DateTime TimeStamp { get; set; }
    }
}
