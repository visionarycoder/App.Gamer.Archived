using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Gamer.StandAlone.Components.Access.GameDefinition;
using Gamer.StandAlone.Components.Access.Tile;

namespace Gamer.StandAlone.Components.Manager.Game
{

    public interface IGameManager
    {
        
        Task<GameDefinition[]> GetGames();
        Task<Tile> GetAutoPlayTurnInput(Guid gameSessionId, Guid playerId);
        Task<ValidationResult> ValidateInput(Guid gameSessionId, string address);
        Task ApplyTurn(Tile tile);
        Task<bool> CheckGameStatus(Guid gameSession);

    }

}