using System;

namespace TicTacToe.Access.GameDefinition
{
    public class GameDefinition
    {

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] GamePieces { get; set; }

    }
}