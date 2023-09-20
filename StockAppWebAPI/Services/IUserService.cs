using StockAppWebApi.Models;
using StockAppWebAPI.ViewModels;

namespace StockAppWebAPI.Services
{
    public interface IUserService
    {
        Task<User?> Register(RegisterViewModel registerViewModel);
        Task<string> Login(LoginViewModel loginViewModel);
    }
}
