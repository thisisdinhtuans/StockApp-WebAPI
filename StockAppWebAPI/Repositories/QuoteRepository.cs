using Microsoft.EntityFrameworkCore;
using StockAppWebApi.Models;
using StockAppWebAPI.Models;

namespace StockAppWebAPI.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly StockAppContext _context;
        public QuoteRepository(StockAppContext context)
        {
            _context = context;
        }

        public async Task<List<Quote>> GetHistoricalQuotes(int days, int stockId)
        {
            //lấy ra được 2 ngày 
            var fromDate = DateTime.Now.Date.AddDays(-days);
            var toDate = DateTime.Now.Date;
            var historicalQuotes = await _context.Quotes
                .Where(q => q.TimeStamp >= fromDate && q.TimeStamp <= toDate
                        && q.StockId == stockId) // Kiểm tra stock_id
                .GroupBy(q => q.TimeStamp.Date) // Nhóm theo ngày
                                                //mapping
                 //chúng ta cần 2 cột là TimeStamp và giá. mục đích của dòng 26 là để vẽ đồ thị<tung price, hoành là Timestamp>
                .Select(g => new Quote
                {
                    TimeStamp = g.Key,
                    Price = g.Average(q => q.Price), // Lấy giá trị trung bình của cùng một ngày
                    //dùng để vẽ đồ thị giá theo ngày
                    // Các thuộc tính khác của Quote nếu cần thiết
                })
                .OrderBy(q => q.TimeStamp) // Sắp xếp theo thứ tự tăng dần về ngày tháng
                .ToListAsync();//để ra được arraylist thì mới có thể hiển thị danh sách các đối tượng được

            return historicalQuotes;
        }

        public async Task<List<RealtimeQuote>?> GetRealtimeQuotes(
                                            int page, 
                                            int limit, 
                                            string sector, 
                                            string industry)
        {
            var query = _context.RealtimeQuotes
                            .Skip((page - 1) * limit) // Bỏ qua số lượng bản ghi trước trang hiện tại
                            .Take(limit); // Lấy số lượng bản ghi tối đa trên mỗi trang
            if (!string.IsNullOrEmpty(sector))
            {
                query = query.Where(q => (q.Sector ?? "").ToLower().Equals(sector.ToLower()));
            }

            if (!string.IsNullOrEmpty(industry))
            {
                query = query.Where(q => q.Industry == industry);
            }
            var quotes = await query.ToListAsync();
            return quotes;
        }
    }
}
