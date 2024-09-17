using System.ComponentModel.DataAnnotations;

namespace Client.Content.ConsoleApp
{

    public class GameState : Entity
    {
        public char[] Board { get; set; }
        public int CurrentPlayerIndex { get; set; }

        public GameState()
        {
            Board = new char[9];
            CurrentPlayerIndex = 0;
        }
    }

}