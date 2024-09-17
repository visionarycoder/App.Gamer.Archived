using System.Linq;

namespace Client.Content.ConsoleApp
{
    public class GameRepository
    {
        private readonly GameContext _context;

        public GameRepository()
        {
            _context = new GameContext();
        }

        public GameState LoadGame()
        {
            return _context.GameStates.FirstOrDefault() ?? new GameState();
        }

        public void SaveGame(GameState gameState)
        {
            var existingGameState = _context.GameStates.FirstOrDefault();
            if (existingGameState != null)
            {
                _context.GameStates.Remove(existingGameState);
            }
            _context.GameStates.Add(gameState);
            _context.SaveChanges();
        }

        public void DeleteGame(GameState gameState)
        {
            var existingGameState = _context.GameStates.FirstOrDefault();
            if (existingGameState != null)
            {
                _context.GameStates.Remove(existingGameState);
                _context.SaveChanges();
            }
        }
    }
}
