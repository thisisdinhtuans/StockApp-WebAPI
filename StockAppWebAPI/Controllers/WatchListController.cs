using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StockAppWebApi.Attributes;
using StockAppWebAPI.Extensions;
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
        public WatchListController(IWatchListService watchlistService, IStockService stockService, IUserService userService)
        {
            _watchlistService = watchlistService;
            _userService = userService;
            _stockService = stockService;
        }
        [HttpPost("AddStockToWatchList/{stockId}")]
        [JwtAuthorize]
        public async Task<IActionResult> AddStockToWatchlist(int stockId)
        {
            //Lấy UserId từ context
            int userId = HttpContext.GetUserId();
            
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
            if (existingWatchlistItem != null)
            {
                return BadRequest("Stock is already in watchlist.");
            }
            await _watchlistService.AddStockToWatchlist(userId, stockId);
            return Ok();
        }

        [HttpGet("GetStockByUserId")]
        [JwtAuthorize]
        public async Task<IActionResult> GetStockByUserId(int userId)
        {
            //Lấy UserId từ context

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
            if (existingWatchlistItem != null)
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
