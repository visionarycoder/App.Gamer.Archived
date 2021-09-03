using System;

namespace Access.GameSession.Interface
{
    
    public class GameSession
    {
        public Guid Id { get; set; }
        public Guid[] Players { get; set; }
        public GameState GameState { get; set; }
    }

}