using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access.GameSession.Interface;

namespace Access.GameSession.Service
{
    
    public static class GameSessionFactory
    {

        public static Interface.GameSession Create()
        {
            var target = new Interface.GameSession
            {
                Id = Guid.NewGuid(),
                GameState = GameState.New,
                Players = Array.Empty<Guid>(),
            };
            return target;
        }

    }

}
