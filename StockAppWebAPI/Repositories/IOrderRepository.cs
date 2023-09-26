using StockAppWebApi.ViewModels;
using StockAppWebAPI.Models;

namespace StockAppWebAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(OrderViewModel orderViewModel);
    }
}
