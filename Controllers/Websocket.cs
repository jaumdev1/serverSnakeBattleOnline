using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SnakeSpaceBattle.Domain;
using SnakeSpaceBattle.Service.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeSpaceBattle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebsocketController : ControllerBase
    {

        private readonly ISnakeService _snakeService;

        public WebsocketController(ISnakeService snakeService)
        {
            _snakeService = snakeService;
        }
        // Dicionário concorrente para armazenar todos os clientes WebSocket conectados
        private static ConcurrentDictionary<Guid, WebSocket> _webSockets = new ConcurrentDictionary<Guid, WebSocket>();


        [HttpGet("CreateSnake")]
        public async Task CreateSnake()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                Guid clientId = Guid.NewGuid();
                _webSockets.TryAdd(clientId, webSocket);
                
                var snake = new Snake();
                snake.Id = clientId;
                snake.MoveDirection = new MoveDirection()
                {
                    AxleX = 1,
                    AxleY = 0,

                };
                snake.Coordinate = new Coordinate()
                {
                    CoordX = 10,
                    CoordY = 10,
                };

                _snakeService.CreateSnake(snake);

                byte[] responseBuffer = Encoding.UTF8.GetBytes(clientId.ToString());
                //addicione a evento setupId aqui para ouvir no cliente depois
                await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);

            }
            else
            {
                HttpContext.Response.StatusCode = 400;

        
            }
        }

        [HttpGet("SendSnake")]
        public async Task SendSnake()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
               

               
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
        private async Task HandleWebSocket(Guid clientId)
        {
            WebSocket webSocket;
            if (!_webSockets.TryGetValue(clientId, out webSocket))
            {
                return;
            }

         

            var buffer = new byte[1024];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if(result.MessageType == WebSocketMessageType.Text)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Snake snake = JsonConvert.DeserializeObject<Snake>(receivedMessage);
                
            }
            while (!result.CloseStatus.HasValue)
            {
             

                var ListSnakes = _snakeService.GetSnakes();
                var jsonListSnakes = JsonConvert.SerializeObject(ListSnakes);

                byte[] responseBuffer = Encoding.UTF8.GetBytes(jsonListSnakes);
                

                foreach (var kvp in _webSockets)
                {
                    var socket = kvp.Value;


                    await socket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            _webSockets.TryRemove(clientId, out _);
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private async Task SnakeSet(Guid clientId)
        {
           
                WebSocket webSocket;
            if (!_webSockets.TryGetValue(clientId, out webSocket))
            {
                return;
            }
            var buffer = new byte[1024];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        }
    }
}