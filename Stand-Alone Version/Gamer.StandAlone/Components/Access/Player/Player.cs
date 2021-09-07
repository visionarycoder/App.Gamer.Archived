using System;

namespace Gamer.StandAlone.Components.Access.Player
{
    public class Player : IPlayer
    {
        
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        public bool IsMachine { get; }
        public string GamePiece { get; }

        public Player(string name, string gamePiece, bool isMachine = false)
        {
            Name = name;
            GamePiece = gamePiece;
            IsMachine = isMachine;
        }

    }
}