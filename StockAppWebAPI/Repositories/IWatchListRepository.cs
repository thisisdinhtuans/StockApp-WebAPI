using StockAppWebAPI.Models;

namespace StockAppWebAPI.Repositories
{
    public interface IWatchListRepository
    {
        Task AddStockToWatchlist(int userId, int stockId);
        Task<WatchList?> GetWatchlist(int userId, int stockId);
    }
}
