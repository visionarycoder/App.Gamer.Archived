using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamer.Client.ConsoleApp.Data;
using static Gamer.Client.ConsoleApp.Manager.Game.GameManager;

namespace Gamer.Client.ConsoleApp.Manager.Game
{
    internal class GameManager
    {

        public class Player
        {

            public Guid Id { get; init; }
            public string Name { get; init; }
            public bool IsMachine { get; init; }
            public string GamePiece { get; init; }

        }

        public class GameDefinition
        {

            public Guid Id { get; init; }
            public string Name { get; init; }
            public string Description { get; init; }
            public string[] GamePieces { get; init; }
            public string TurnPrompt { get; init; }
            public int MaxNumberOfPlayers { get; init; }
            public int MinNumberOfPlayers { get; init; }
            public string[] Tags { get; init; }

        }


        public interface IGameManager
        {

            Task<GameDefinition[]> GetGames();
            Task AutoPlayTurn(Guid gameSessionId);
            Task ApplyTurn(Guid gameSessionId, Guid playerId, string address);
            Task<ValidationResult> ConfirmUsableAddress(Guid gameSessionId, string address);
            Task<bool> IsGamePlayable(Guid gameSessionId);
            Task<Guid> StartGame(Guid gameDefinitionId, int playerCount);
            Task<DataTable> GetBoard(Guid gameSessionId);
            Task<string> GetTurnPrompt(Guid gameSessionId);
            Task<Player> GetCurrentPlayer(Guid gameSessionId);
            Task<Player> FindWinner(Guid gameSessionId);
        }

        internal class GameManager : IGameManager
        {
            public async Task<GameDefinition[]> GetGames()
            {
                throw new NotImplementedException();
            }

            public async Task AutoPlayTurn(Guid gameSessionId)
            {
                throw new NotImplementedException();
            }

            public async Task ApplyTurn(Guid gameSessionId, Guid playerId, string address)
            {
                throw new NotImplementedException();
            }

            public async Task<ValidationResult> ConfirmUsableAddress(Guid gameSessionId, string address)
            {
                throw new NotImplementedException();
            }

            public async Task<bool> IsGamePlayable(Guid gameSessionId)
            {
                throw new NotImplementedException();
            }

            public async Task<Guid> StartGame(Guid gameDefinitionId, int playerCount)
            {
                throw new NotImplementedException();
            }

            public async Task<DataTable> GetBoard(Guid gameSessionId)
            {
                throw new NotImplementedException();
            }

            public async Task<string> GetTurnPrompt(Guid gameSessionId)
            {
                throw new NotImplementedException();
            }

            public async Task<Player> GetCurrentPlayer(Guid gameSessionId)
            {
                throw new NotImplementedException();
            }

            public async Task<Player> FindWinner(Guid gameSessionId)
            {
                throw new NotImplementedException();
            }
        }
    }
}
