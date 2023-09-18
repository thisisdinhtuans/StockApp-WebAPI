using StockAppWebApi.Models;
using StockAppWebAPI.Repositories;

namespace StockAppWebAPI.Services
{
    public class UserService: IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Register(User user)
        {
            //kiểm tra xem username hoặc email đã tồn tại trong database hay chưa
            var existingUserByUsername=await _userRepository.GetByUsername(user.Username);
            if(existingUserByUsername == null)
            {
                throw new ArgumentException("Username already exists");
            }

            var existingUserByEmail=await _userRepository.GetByEmail(user.Email);
            if(existingUserByEmail == null)
            {
                throw new ArgumentException("Email already exits");
            }

            //Thực hiện thêm mới user
            return await _userRepository.Create(user);
        }
    }
}
