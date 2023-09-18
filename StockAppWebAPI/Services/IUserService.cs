using StockAppWebApi.Models;

namespace StockAppWebAPI.Services
{
    public interface IUserService
    {
        Task<int> Register(User user);
    }
}
