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
using Microsoft.AspNetCore.SignalR;
using SnakeSpaceBattle.Hubs;
using SnakeSpaceBattle.Service;

namespace SnakeSpaceBattle.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebsocketController : ControllerBase
    {

        private readonly IGameService _snakeService;
        private readonly IHubContext<GameHub> _snakeHubContext;
        public WebsocketController(IGameService snakeService, IHubContext<GameHub> snakeHubContext)
        {
            _snakeService = snakeService;
            _snakeHubContext = snakeHubContext;
        }
        
        private static ConcurrentDictionary<Guid, WebSocket> _webSockets = new ConcurrentDictionary<Guid, WebSocket>();


        [HttpGet("CreateSnake")]
        public async Task<IActionResult> CreateSnake()
        {
    
            string connectionId = Request.Headers["Connection-Id"];
     
            var snake = new Snake()
            {
                Id = connectionId,
                MoveDirection = new MoveDirection()
                {
                    AxleX = 1,
                    AxleY = 0,
                },
                Coordinate = new Coordinate()
                {
                    CoordX = 10,
                    CoordY = 10,
                }
            };

            _snakeService.AddSnake(snake);

            //await _snakeHubContext.Clients.Client(connectionId).SendAsync("SnakeCreated", connectionId);
            SendStatusGame();

            return Ok(connectionId);
        }

        [HttpGet("SendSnake")]
        public async Task SendStatusGame()
        {
            Game game = _snakeService.GetGame();
            await _snakeHubContext.Clients.All.SendAsync("StatusGame", game);
        }

    }
}