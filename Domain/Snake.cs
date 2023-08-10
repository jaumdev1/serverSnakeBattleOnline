using System.Numerics;

namespace SnakeSpaceBattle.Domain
{
    public class Snake
    {
        public string Id { get; set; } 
         public string name { get; set; }

         public Coordinate Coordinate { get; set; }

         public MoveDirection MoveDirection { get; set; }

    }
}
