using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.StandAlone.Components.Access.GameDefinition
{
    public class GameDefinitionAccess : IGameDefinitionAccess
    {

        private static readonly HashSet<GameDefinition> cache;

        static GameDefinitionAccess()
        {

            var ticTacToe = new GameDefinition
            {
                Name = "Tic-Tac-Toe",
                Description = "The Classic three across game.  Also known as 'noughts and crosses' or 'Xs and Os.'",
                GamePieces = new[] { "X", "O" },
                TurnPrompt = "Your turn."
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