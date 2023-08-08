using SnakeSpaceBattle.Domain;

namespace SnakeSpaceBattle.Service.Interfaces
{
    public interface ISnakeService
    {
        List<Snake> GetSnakes();
        Snake CreateSnake(Snake snake);
    }
}
