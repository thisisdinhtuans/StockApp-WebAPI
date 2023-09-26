using Microsoft.AspNetCore.Mvc;
using StockAppWebAPI.Models;
using StockAppWebAPI.Services;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace StockAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;
        public QuoteController(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }
        [HttpGet("ws")]
        // đường link đến webservice https://localhost:7070/api/quote/ws
        public async Task GetRealtimeQuotes(
            int page=1, int limit=10,
            string sector="",
            string industry=""
            )
        {
            if (HttpContext.WebSockets.IsWebSocketRequest) //check xem có phải request của web socket không
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                //await _webSocketManager.AddWebSocket(webSocket);
                while (webSocket.State == WebSocketState.Open) //dòng này được hiểu kết nối còn tồn tại thì nó lặp liên tục
                {
                    List<RealtimeQuote>? quotes = await _quoteService
                                                    .GetRealtimeQuotes(page, limit, sector, industry);
                    //lấy dữ liệu real time dưới dạng service, service gọi repository
                    //đầu tiên tạo service trước và tạo repository sau.

                    //convert list of objects to json, tức là chuyển đổi đối tượng quotes thành json, dùng  hàm Serialize để chuyển đổi đối tượng quote thành json
                    string jsonString = JsonSerializer.Serialize(quotes);
                    var buffer = Encoding.UTF8.GetBytes(jsonString);

                    //SendAsync là gửi ngược dữ liệu trở về client từ Controller, từ server gửi đén client
                    //đưa đống json string vào buffer(khi inspect ở testWebsocket.html), sau đó gửi lại cho client, sau đó thì chơi 2 giây
                    await webSocket.SendAsync(new ArraySegment<byte>(buffer),
                        WebSocketMessageType.Text, true, CancellationToken.None);

                    await Task.Delay(2000); //đợi 2 giây trước khi gửi giá trị tiếp theo
                }
                //khi mà kết nối của chúng ta kết thúc vì lý do gì đó thì phía client nó không cho next vào internet hoặc không connect đến server nữa thì chúng ta sẽ gọi hàm closeAsync
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection closed by the server", CancellationToken.None);
            }
            else
            {
                HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }
        [HttpGet("historical")]
        public async Task<IActionResult> GetHistoricalQuotes(int days, int stockId)
        {
            var historicalQuotes = await _quoteService.GetHistoricalQuotes(days, stockId);
            return Ok(historicalQuotes);
        }
    }
}
