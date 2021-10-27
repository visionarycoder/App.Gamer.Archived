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
			cache = new HashSet<GameDefinition> {ticTacToe};

		}

		public async Task<GameDefinition[]> FindGameDefinitions(Func<GameDefinition, bool> filter)
		{

			var results = cache.Where(filter);
			return await Task.FromResult(results.ToArray());

		}

	}

}