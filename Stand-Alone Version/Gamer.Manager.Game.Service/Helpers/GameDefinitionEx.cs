using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamer.Manager.Game.Interface;

namespace Gamer.Manager.Game.Service.Helpers
{
	
	public static class GameDefinitionEx
	{

		public static GameDefinition Convert(this Access.GameDefinition.Interface.GameDefinition source)
		{
			var target = new GameDefinition
			{
				Description = source.Description,
				Id = source.Id,
				GamePieces = source.GamePieces,
				MaxNumberOfPlayers = source.MaxNumberOfPlayers,
				MinNumberOfPlayers = source.MinNumberOfPlayers,
				Name = source.Name,
				TurnPrompt = source.TurnPrompt
			};
			return target;
		}

		public static GameDefinition[] Convert(this Access.GameDefinition.Interface.GameDefinition[] source)
		{
			return source.Select(i => i.Convert()).ToArray();
		}

	}
}
