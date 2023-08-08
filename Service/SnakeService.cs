using SnakeSpaceBattle.Domain;
using SnakeSpaceBattle.Service.Interfaces;

namespace SnakeSpaceBattle.Service
{
    public class SnakeService : ISnakeService
    {
        private readonly List<Snake> _snakes = new List<Snake>();

        public List<Snake> GetSnakes()
        {
            return _snakes;
        }

        public Snake CreateSnake(Snake snake)
        {
           
            _snakes.Add(snake);
            return snake;
        }
    }
}
