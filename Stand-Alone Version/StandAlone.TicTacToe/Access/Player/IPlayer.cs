using System;

namespace TicTacToe.Access.Player
{
    public interface IPlayer
    {
        Guid Id { get; }
        string Name { get; }
        bool IsMachine { get; }
        string GamePiece { get; }
    }
}