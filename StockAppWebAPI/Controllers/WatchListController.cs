using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StockAppWebApi.Filters;
using StockAppWebAPI.Services;
using System.Net;
using System.Security.Claims;

namespace StockAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchListController : Controller
    {
        private readonly IWatchListService _watchlistService;
        private readonly IUserService _userService;
        private readonly IStockService _stockService;
        private readonly AuthorizationFilterContext _context;
        public WatchListController(IWatchListService watchlistService, IStockService stockService, IUserService userService, AuthorizationFilterContext authorizationFilterContext)
        {
            _watchlistService = watchlistService;
            _userService = userService;
            _stockService = stockService;
            _context = authorizationFilterContext;
        }
        [HttpPost("AddStockToWatchList/{stockId}")]
        [JwtAuthorize]
        public async Task<IActionResult> AddStockToWatchlist(int stockId)
        {
            //Lấy UserId từ context
            if(!int.TryParse(_context.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)) //out var userId: tức là lấy ra được userId
            {
                return Unauthorized();
            }
            var user = await _userService.GetUserById(userId);
            var stock = await _stockService.GetStockById(stockId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (stock == null)
            {
                return NotFound("Stock not found");
            }
            //Kiểm tra cổ phiểu đã tồn tại trong watchlist của người dùng chưa  
            var existingWatchlistItem = await _watchlistService.GetWatchlistItem(userId, stockId); 
            if (existingWatchlistItem == null)
            {
                return BadRequest("Stock is already in watchlist.");
            }
            await _watchlistService.AddStockToWatchlist(userId, stockId);
            return Ok();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
