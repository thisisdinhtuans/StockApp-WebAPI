using StockAppWebApi.Models;

namespace StockAppWebAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);
        Task<User> GetByUsername(string username);
        Task<User> GetByEmail(string email);
        Task<int> Create(User user);

    }
}
