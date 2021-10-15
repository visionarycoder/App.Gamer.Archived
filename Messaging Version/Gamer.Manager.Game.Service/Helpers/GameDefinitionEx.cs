using System.Diagnostics.Contracts;
using System.Linq;
using Gamer.Manager.Game.Interface;

namespace Gamer.Manager.Game.Service.Helpers
{
	
	public static class GameDefinitionEx
	{

		public static GameDefinition Convert(this Access.GameDefinition.Interface.GameDefinition source)
		{
			Contract.Assert(source != null, "Input is null.");
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
			Contract.Assert(source != null, "Input is null.");
			return source.Select(i => i.Convert()).ToArray();
		}

	}
}
