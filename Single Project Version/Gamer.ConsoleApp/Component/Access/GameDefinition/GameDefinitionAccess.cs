using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.Component.Access.GameDefinition
{
    public class GameDefinitionAccess : IGameDefinitionAccess
    {

        private static readonly HashSet<GameDefinition> cache;

        static GameDefinitionAccess()
        {

            var ticTacToe = new GameDefinition
            {
                Id = Guid.NewGuid(),
                Name = "Tic-Tac-Toe",
                Description = "The Classic three across game.  Also known as 'noughts and crosses' or 'Xs and Os.'",
                GamePieces = new[] { "X", "O" },
                MaxNumberOfPlayers = 2,
                MinNumberOfPlayers = 0,
                TurnPrompt = "Your turn.",
            };
            cache = new HashSet<GameDefinition> { ticTacToe };

        }

        public async Task<GameDefinition> RetrieveGameDefinition(Guid gameDefinitionId)
        {
            var result = cache.FirstOrDefault(i => i.Id == gameDefinitionId);
            return await Task.FromResult(result);
        }

        public async Task<GameDefinition[]> FindGameDefinitions(Func<GameDefinition, bool> filter)
        {

            var results = filter != null ? cache.Where(filter) : cache.AsEnumerable();
            return await Task.FromResult(results.ToArray());

        }


    }

    public class GameDefinitionCriteria
    {

        public Guid? Id { get; init; }
        public bool ById => Id != null;

        public Guid?[] PlayerIds { get; init; }
        public bool ByPlayerId => PlayerIds != null && PlayerIds.Any();

        public int? MaxNumberOfPlayers { get; init; }
        public bool ByMaxNumberOfPlayers => MaxNumberOfPlayers is > 0;

        public int MinNumberOfPlayers { get; init; }
        public bool ByMinNumberOfPlayers => MinNumberOfPlayers is > -1;

    }

}