using System.Collections.Generic;

namespace AuthTokenProject
{
    public class Game : IGameItem
    {
        public int ID { get; set; }
        public User Orgnaizator { get; set; }
        public string Token { get; internal set; }

        public readonly List<User> Players;

        public Game()
        {
            Players = new List<User>();
        }
    }
}
