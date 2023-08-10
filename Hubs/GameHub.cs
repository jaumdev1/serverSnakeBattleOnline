using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SnakeSpaceBattle.Domain;
using SnakeSpaceBattle.Service;
using SnakeSpaceBattle.Service.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SnakeSpaceBattle.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameService _gameService;
        public GameHub(IGameService gameService)
        {
            _gameService = gameService;
          
        }
       
        public async Task SnakeCreate()
        {
        var snake =  new Snake();
        string callerConnectionId = Context.ConnectionId;
        snake.Id = callerConnectionId;

        _gameService.AddSnake(snake);
         Console.WriteLine("Snake Create: " + snake.Id);
        
        await Clients.Caller.SendAsync("YourCreateSnake", snake);

        }

        public async Task UpdateToSnake(Snake snake)
        {
           await _gameService.UpdateSnake(snake);
            Console.WriteLine("Update To Snake: " + snake.Id);
           UpdateStatus();
        }

        public async Task UpdateStatus()
        {
            Game game = _gameService.GetGame();
            var json = JsonConvert.SerializeObject(game);
            await Clients.All.SendAsync("UpdateToGame", json);
        }


    }
}