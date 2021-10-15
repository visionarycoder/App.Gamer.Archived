using System;

namespace Gamer.Access.GameSession.Interface
{

	public class GameSession
	{

		public Guid Id { get; init; }
		public Guid[] PlayerIds { get; init; }
		public Guid GameDefinitionId { get; init; }
		public Guid CurrentPlayerId { get; set; }

	}

}