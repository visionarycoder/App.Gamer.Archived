using System;

namespace Gamer.StandAlone.Components.Access.Player
{
    public interface IPlayer
    {
        Guid Id { get; }
        string Name { get; }
        bool IsMachine { get; }
        string GamePiece { get; }
    }
}