using System;

namespace Gamer.Manager.Game.Interface
{

	public class GameDefinition
	{

		public Guid Id { get; init; }
		public string Name { get; init; }
		public string Description { get; init; }
		public string[] GamePieces { get; init; }
		public string TurnPrompt { get; init; }
		public int MaxNumberOfPlayers { get; init; }
		public int MinNumberOfPlayers { get; init; }
		public string[] Tags { get; init; }

	}

}