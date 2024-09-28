using Client.Content.ConsoleApp.Access;
using Client.Content.ConsoleApp.Engine;
using Client.Content.ConsoleApp.Resources.Data.GamerDB.Models;

namespace Client.Content.ConsoleApp.Managers
{
    public class ContentManager
    {
        private readonly GamePlayEngine gamePlayEngine;
        private readonly GamesAccess gamesAccess;

        public ContentManager(GamePlayEngine gamePlayEngine, GamesAccess gamesAccess)
        {
            this.gamePlayEngine = gamePlayEngine;
            this.gamesAccess = gamesAccess;
        }

        public async Task<bool> MakeMoveAsync(int gameStateId, int move)
        {
            var gameState = gamesAccess.LoadGameById(gameStateId);
            var players = gameState.Players.ToArray();
            var result = await gamePlayEngine.MakeMoveAsync(gameState, players, move);
            gamesAccess.SaveGame(gameState); // Save the updated game state
            return result;
        }

        public async Task<bool> CheckWinConditionAsync(int gameStateId)
        {
            var gameState = gamesAccess.LoadGameById(gameStateId);
            return await gamePlayEngine.CheckWinConditionAsync(gameState);
        }

        public bool IsBoardFull(int gameStateId)
        {
            var gameState = gamesAccess.LoadGameById(gameStateId);
            return gamePlayEngine.IsBoardFull(gameState);
        }

        public Session LoadGame(int gameStateId)
        {
            return gamesAccess.LoadGameById(gameStateId);
        }

        public void SaveGame(int gameStateId)
        {
            var gameState = gamesAccess.LoadGameById(gameStateId);
            gamesAccess.SaveGame(gameState);
        }

        public void DeleteGame(int gameStateId)
        {
            var gameState = gamesAccess.LoadGameById(gameStateId);
            gamesAccess.DeleteGame(gameState);
        }

        public List<Definition> GetAvailableGames()
        {
            return gamesAccess.GetAvailableGames();
        }
    }
}
