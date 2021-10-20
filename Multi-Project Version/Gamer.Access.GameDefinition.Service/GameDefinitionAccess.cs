using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.GameDefinition.Interface;

namespace Gamer.Access.GameDefinition.Service
{
	public class GameDefinitionAccess : IGameDefinitionAccess
	{

		private static readonly HashSet<Interface.GameDefinition> cache;

		static GameDefinitionAccess()
		{
		
			var ticTacToe = new Interface.GameDefinition
			{
				Id = Guid.NewGuid(),
				Name = "Tic-Tac-Toe",
				Description = "The Classic three across game.  Also known as 'noughts and crosses' or 'Xs and Os.'",
				GamePieces = new[] { "X", "O" },
				MaxNumberOfPlayers = 2,
				MinNumberOfPlayers = 0,
				TurnPrompt = "Your turn.",
			};
			cache = new HashSet<Interface.GameDefinition> {ticTacToe};

		}

		public async Task<Interface.GameDefinition[]> GetGameDefinitions()
		{

			return await Task.FromResult(cache.ToArray());

		}

		public async Task<Interface.GameDefinition> GetGameDefinition(Guid gameDefinitionId)
		{

			var gameDefinition = cache.FirstOrDefault(i => i.Id == gameDefinitionId);
			return await Task.FromResult(gameDefinition);

		}

		public async Task<Interface.GameDefinition[]> FindGameDefinitions(Func<Interface.GameDefinition, bool> filter)
		{

			var results = cache.Where(filter);
			return await Task.FromResult(results.ToArray());

		}

	}

}