using System;

namespace Gamer.StandAlone.Components.Access.Tile
{
    public class Tile
    {

        public Guid Id { get; set; }
        public Guid GameSessionId { get; set;}
        public string Address { get; set; }
        public Guid PlayerId { get; set; } = Guid.Empty;
        public bool IsEmpty => PlayerId == Guid.Empty;

        public Tile(string address, Guid gameSessionId)
        {
            GameSessionId = gameSessionId;
            Address = address;
        }

    }
}