using System;
using System.Threading.Tasks;
using Gamer.StandAlone.Components.Access.Tile;

namespace Gamer.StandAlone.Components.Engine.AutoPlay
{
    
    public interface IAutoPlayEngine
    {
        Task<Tile> PlayTurn(Guid gameSessionId, Guid playerId);
    }

}