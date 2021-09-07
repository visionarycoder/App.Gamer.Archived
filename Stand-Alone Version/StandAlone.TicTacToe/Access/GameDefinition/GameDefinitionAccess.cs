using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Access.GameDefinition
{
    public class GameDefinitionAccess : IGameDefinitionAccess
    {

        private static readonly HashSet<GameDefinition> cache;

        static GameDefinitionAccess()
        {
            
            var ticTacToe = new GameDefinition
            {
                Name = "Tic-Tac-Toe",
                Description = "Classic Game.",
                GamePieces = new[] {"X", "O"},
            };

            cache = new HashSet<GameDefinition>
            {
                ticTacToe
            };

        }

        public async Task<GameDefinition[]> GetGameDefinitions()
        {
            return await Task.FromResult(cache.ToArray());
        }

    }
}
