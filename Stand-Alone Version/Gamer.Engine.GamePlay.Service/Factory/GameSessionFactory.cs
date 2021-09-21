using System;
using System.Linq;
using Gamer.Engine.GamePlay.Interface;

namespace Gamer.Engine.GamePlay.Service.Factory
{

	public static class GameSessionFactory
	{
	
		public static GameSession Create(Guid gameDefinitionId, Guid[] playerIds)
		{
		
			var target = new GameSession
			{
				Id = Guid.NewGuid(),
				CurrentPlayerId = playerIds.First(),
				GameDefinitionId = gameDefinitionId,
				PlayerIds = playerIds,
			};
			return target;

		}

	}

}