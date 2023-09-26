using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace StockAppWebAPI.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/ws")]
    public class WebSocketController:ControllerBase
    {
        [HttpGet("api/ws")]
        public async Task Get()
        {
            //Kiểm tra xem có phải websocket request không, có thì xử lý còn không thì thôi 
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket=await HttpContext.WebSockets.AcceptWebSocketAsync();
                //sinh ngẫu nhiên 2 giá trị x, y, thay đổi 2 giây trên lần(đúng như kiểu dữ liệu real time)
                var random =new Random();
                while(webSocket.State==WebSocketState.Open) //dòng này được hiểu kết nối còn tồn tại thì nó lặp liên tục
                {
                    //tạo 2 giá trị x, y ngẫu nhiên
                    int x=random.Next(1, 100);
                    int y = random.Next(1, 100);
                    //biến đổi chỗ code này thành chuỗi json
                    var buffer = Encoding.UTF8.GetBytes($"{{ \"x\": {x}, \"y\": {y}}}");
                    //gọi hàm send để gửi dữ liệu này về client, gửi đối tượng buffer naỳ về có kiểu text. Nơi nhận sẽ pạc kiểu text này thành đối tượng 
                    Console.WriteLine($"x: {x}, y: {y}");
                    await webSocket.SendAsync(
                        new ArraySegment<byte>(buffer),
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
    }
}
