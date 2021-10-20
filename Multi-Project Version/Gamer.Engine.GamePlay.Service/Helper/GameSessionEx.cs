using System.Collections.Generic;
using System.Linq;
using Gamer.Engine.GamePlay.Interface;

namespace Gamer.Engine.GamePlay.Service.Helper
{
	
	internal static class GameSessionEx
	{

		public static GameSession Convert(this Access.GameSession.Interface.GameSession source)
		{
	
			var target = new GameSession
			{
				CurrentPlayerId = source.CurrentPlayerId,
				GameDefinitionId = source.GameDefinitionId,
				Id = source.Id,
				PlayerIds = source.PlayerIds
			};
			return target;

		}

		public static Access.GameSession.Interface.GameSession Convert(this GameSession source)
		{

			var target = new Access.GameSession.Interface.GameSession
			{
				CurrentPlayerId = source.CurrentPlayerId,
				GameDefinitionId = source.GameDefinitionId,
				Id = source.Id,
				PlayerIds = source.PlayerIds
			};
			return target;

		}

		public static GameSession[] Convert(this IEnumerable<Access.GameSession.Interface.GameSession> source)
		{
			return source.Select(i => i.Convert()).ToArray();
		}

		public static Access.GameSession.Interface.GameSession[] Convert(this IEnumerable<GameSession> source)
		{
			return source.Select(i => i.Convert()).ToArray();
		}

	}

}