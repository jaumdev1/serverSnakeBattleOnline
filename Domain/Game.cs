using System.Runtime.Serialization;

namespace SnakeSpaceBattle.Domain
{
    [DataContract]
    public class Game
    {
        [DataMember]
        public readonly List<Snake> snakes = new List<Snake>();
        [DataMember]
        public readonly List<Food> foods = new List<Food>();

        
    }
}
