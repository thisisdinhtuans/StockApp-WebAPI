using StockAppWebApi.Models;
using StockAppWebAPI.Models;

namespace StockAppWebAPI.Services
{
    public interface IQuoteService
    {
        //RealtimeQuote là cái dữ liệu trả về
        //tại sao để dấu hỏi ở đây, nó có thể trả về list rỗng nên nó là optional list, List<RealtimeQuote>?
        Task<List<RealtimeQuote>?> GetRealtimeQuotes(
            int page,
            int limit,
            string sector,
            string industry
            );
        Task<List<Quote>> GetHistoricalQuotes(int days, int stockId);
    }
}
