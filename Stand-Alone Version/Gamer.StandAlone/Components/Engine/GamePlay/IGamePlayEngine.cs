using System;
using System.Threading.Tasks;

namespace Gamer.StandAlone.Components.Engine.GamePlay
{

    public interface IGamePlayEngine
    {
    
        Task<bool> IsPlayable(Guid gameSessionId);
        Task<Guid> FindWinner(Guid gameSessionId);
        Task<bool> IsTileOpen(Guid gameSessionId, string address);

    }

}