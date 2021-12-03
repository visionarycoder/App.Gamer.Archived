using System;
using System.Linq;

namespace Gamer.Component.Access.GameSession
{

	public class GameSession
	{

		public Guid Id { get; init; }
		public Guid[] PlayerIds { get; init; }
		public Guid GameDefinitionId { get; init; }
		public Guid CurrentPlayerId { get; set; }

	}

    public class GameSessionCriteria
    {
		public Guid? Id { get; set; }
        public bool ById => Id != null;

        public Guid?[] PlayerIds { get; set; }
        public bool ByPlayerIds => PlayerIds != null && PlayerIds.Any();
    }
}