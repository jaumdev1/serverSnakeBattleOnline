using SnakeSpaceBattle.Domain;
using SnakeSpaceBattle.Service.Interfaces;

namespace SnakeSpaceBattle.Service
{
    public class GameService : IGameService
    {
        private readonly Game game = new Game();
       public Game GetGame()
        {
            return game;
        }

        public void AddSnake(Snake snake)
        {
            game.snakes.Add(snake);
       
        }
        public async Task UpdateSnake(Snake snake)
        {
           var snk = game.snakes.Where(p => p.Id == snake.Id).SingleOrDefault();

            if (snk != null) {
                snk.Coordinate= snake.Coordinate;
                snk.MoveDirection= snake.MoveDirection;
            }
        }
    }
}
