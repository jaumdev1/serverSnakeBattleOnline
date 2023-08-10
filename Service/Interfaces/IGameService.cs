using SnakeSpaceBattle.Domain;

namespace SnakeSpaceBattle.Service.Interfaces
{
    public interface IGameService
    {
        Game GetGame();
        void AddSnake(Snake snake);
        Task UpdateSnake(Snake snake);
    }
}
