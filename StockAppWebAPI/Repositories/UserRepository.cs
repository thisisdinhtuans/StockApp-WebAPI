using StockAppWebApi.Models;

namespace StockAppWebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {


        private readonly StockAppContext _context;
        public UserRepository(StockAppContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u=>u.Username==username);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u=>u.Email==email);
        }

        public async Task<int> Create(User user)
        {
            //Đoạn này sẽ gọi procedure trong SQL
            return user.UserId;
        }

    }
}
