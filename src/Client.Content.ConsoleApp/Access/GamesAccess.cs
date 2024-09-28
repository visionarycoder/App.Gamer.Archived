using Client.Content.ConsoleApp.Resources.Data.GamerDB;
using Client.Content.ConsoleApp.Resources.Data.GamerDB.Models;
using Microsoft.EntityFrameworkCore;

namespace Client.Content.ConsoleApp.Access
{
    public class GamesAccess
    {
        private readonly DbContextOptions<GameContext> options;

        public GamesAccess(DbContextOptions<GameContext> options)
        {
            this.options = options;
        }

        private GameContext CreateContext()
        {
            return new GameContext(options);
        }

        public Session LoadGame()
        {
            using (var context = CreateContext())
            {
                return context.GameStates
                    .Include(gs => gs.Board)
                    .Include(gs => gs.Players)
                        .ThenInclude(p => p.GameToken)
                    .Include(gs => gs.GameDefinition)
                    .FirstOrDefault() ?? new Session();
            }
        }

        public void SaveGame(Session gameState)
        {
            using (var context = CreateContext())
            {
                var existingGameState = context.GameStates
                    .Include(gs => gs.Players)
                        .ThenInclude(p => p.GameToken)
                    .FirstOrDefault();

                if (existingGameState != null)
                {
                    context.GameStates.Remove(existingGameState);
                }

                context.GameStates.Add(gameState);
                context.SaveChanges();
            }
        }

        public void DeleteGame(Session gameState)
        {
            using (var context = CreateContext())
            {
                var existingGameState = context.GameStates.FirstOrDefault();
                if (existingGameState != null)
                {
                    context.GameStates.Remove(existingGameState);
                    context.SaveChanges();
                }
            }
        }

        public List<Definition> GetAvailableGames()
        {
            using (var context = CreateContext())
            {
                return context.GameDefinitions
                    .Include(gd => gd.Players)
                        .ThenInclude(p => p.GameToken)
                    .Include(gd => gd.Workflows)
                    .ToList();
            }
        }

        public Session LoadGameById(int gameStateId)
        {
            using (var context = CreateContext())
            {
                return context.GameStates
                    .Include(gs => gs.Board)
                    .Include(gs => gs.Players)
                    .ThenInclude(p => p.GameToken)
                    .Include(gs => gs.GameDefinition)
                    .FirstOrDefault(gs => gs.Id == gameStateId) ?? new Session();
            }
        }
    }
}
