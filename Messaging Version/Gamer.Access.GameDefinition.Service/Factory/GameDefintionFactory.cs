using System;
using System.Collections.Generic;

namespace Gamer.Access.GameDefinition.Service.Factory
{

	public static class GameDefintionFactory
	{

		internal static ICollection<Interface.GameDefinition> Create()
		{

			var list = new List<Interface.GameDefinition>
			{
				new Interface.GameDefinition
				{
					Id = Guid.NewGuid(),
					Name = "Tic-Tac-Toe",
					Description = "The Classic three across game.  Also known as 'noughts and crosses' or 'Xs and Os.'",
					GamePieces = new[] {"X", "O"},
					MaxNumberOfPlayers = 2,
					MinNumberOfPlayers = 0,
					TurnPrompt = "Your turn.",
				}
			};
			return list;

		}

	}

}
