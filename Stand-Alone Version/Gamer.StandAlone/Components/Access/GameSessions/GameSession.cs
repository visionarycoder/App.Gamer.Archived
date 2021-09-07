using System;

namespace Gamer.StandAlone.Components.Access.GameSessions
{
    public class GameSession
    {

        public Guid Id { get; set; }
        public GameStatus GameStatus { get; set; }
        public Guid PlayerIds { get; set; }

    }
}