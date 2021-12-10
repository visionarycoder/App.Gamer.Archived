using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Gamer.Access.GameSession.Interface;
using Gamer.Framework;
using Gamer.Utility.ServiceMessaging;

using Microsoft.Extensions.Logging;

namespace Gamer.Access.GameSession.Service
{

	public class GameSessionAccess : ServiceBase, IGameSessionAccess
	{

		private static readonly HashSet<Interface.GameSession> cache;

		static GameSessionAccess()
		{
			cache = new HashSet<Interface.GameSession>();
		}

		public GameSessionAccess(ILogger logger)
			: base(logger)
		{
		}

		public async Task<CreateGameSessionResponse> CreateGameSessionAsync(CreateGameSessionRequest request)
		{

			var response = ServiceMessageFactory<CreateGameSessionResponse>.CreateFrom(request);
			if (cache.FirstOrDefault(i => i.Id == request.GameSession.Id) == null)
			{
				cache.Add(request.GameSession);
			}
			else
			{
				response.Errors += "Game session already exists.";
				logger.LogError($"Game session ({request.GameSession.Id}) already exists.");
			}
			response.GameSession = request.GameSession;
			return await Task.FromResult(response);

		}

		public async Task<UpdateGameSessionResponse> UpdateGameSessionAsync(UpdateGameSessionRequest request)
		{

			var response = ServiceMessageFactory<UpdateGameSessionResponse>.CreateFrom(request);
			var gameSession = cache.FirstOrDefault(i => i.Id == request.GameSession.Id);
			if (gameSession != null)
			{
				gameSession.CurrentPlayerId = request.GameSession.CurrentPlayerId;
				response.GameSession = gameSession;
			}
			else
			{
				response.Errors += "Unable to find game session to update.";
				logger.LogError($"Unable to find game session ({gameSession.Id}) to update.");
			}
			return await Task.FromResult(response);

		}

		public async Task<GetGameSessionResponse> GetGameSessionAsync(GetGameSessionRequest request)
		{

			var response = ServiceMessageFactory<GetGameSessionResponse>.CreateFrom(request);
			var gameSession = cache.FirstOrDefault(i => i.Id == request.GameSessionId);
			if (gameSession != null)
			{
				response.GameSession = gameSession;
			}
			else
			{
				response.Errors += "Unable to find game session.";
				logger.LogError($"Unable to find game session ({request.GameSessionId}).");
			}
			return await Task.FromResult(response);

		}

	}

}