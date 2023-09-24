using StockAppWebApi.Models;
using StockAppWebAPI.Models;

namespace StockAppWebAPI.Services
{
    public interface IWatchListService
    {
        Task AddStockToWatchlist(int userId, int stockId);
        Task<WatchList?> GetWatchlistItem(int userId, int stockId); 
    }
}
