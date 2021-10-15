using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.GameDefinition.Interface;
using Gamer.Access.GameDefinition.Service.Factory;
using Gamer.Framework;
using Gamer.Framework.ServiceMessaging;
using Microsoft.Extensions.Logging;

namespace Gamer.Access.GameDefinition.Service
{
	public class GameDefinitionAccess : ServiceBase, IGameDefinitionAccess
	{

		private static readonly HashSet<Interface.GameDefinition> cache;

		static GameDefinitionAccess()
		{

			var gameDefinitions = GameDefintionFactory.Create();
			cache = new HashSet<Interface.GameDefinition>(gameDefinitions);

		}

		public GameDefinitionAccess(ILogger logger)
			: base(logger)
		{
		}

		public async Task<GetGameDefinitionsResponse> GetGameDefinitionsAsync(GetGameDefinitionsRequest request)
		{

			var response = ServiceMessageFactory<GetGameDefinitionsResponse>.CreateFrom(request);
			response.GameDefinitions = cache.ToArray();
			if (! response.GameDefinitions.Any())
			{
				response.Errors = "Unable to find any game definitions.";
				logger.LogError($"Initialization Error!  Unable to find any game definitions.");
			}
			return await Task.FromResult(response);

		}

		public async Task<GetGameDefinitionResponse> GetGameDefinitionAsync(GetGameDefinitionRequest request)
		{

			var response = ServiceMessageFactory<GetGameDefinitionResponse>.CreateFrom(request);
			response.GameDefinition = cache.FirstOrDefault(i => i.Id == request.GameDefinitionId);
			return await Task.FromResult(response);

		}

	}

}