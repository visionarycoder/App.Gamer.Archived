using System;
using System.Threading.Tasks;
using Gamer.Access.GameDefinition.Interface;
using Gamer.Access.GameDefinition.Service;
using Gamer.Access.GameSession.Interface;
using Gamer.Access.GameSession.Service;
using Gamer.Access.Player.Interface;
using Gamer.Access.Player.Service;
using Gamer.Access.Tile.Interface;
using Gamer.Access.Tile.Service;
using Gamer.Engine.GameBoard.Interface;
using Gamer.Engine.GameBoard.Service;
using Gamer.Engine.GamePlay.Interface;
using Gamer.Engine.GamePlay.Service;
using Gamer.Engine.Validation.Interface;
using Gamer.Engine.Validation.Service;
using Gamer.Framework;
using Gamer.Framework.Logging;
using Gamer.Manager.Game.Interface;
using Gamer.Manager.Game.Service;
using Microsoft.Extensions.Logging;
using Unity;

namespace Gamer.Client.ConsoleApp
{

	class Program
	{

		static async Task Main(string[] args)
		{

			Console.WriteLine(AppConstant.ApplicationName);

			var container = new UnityContainer();

			container.RegisterType<ILogger, TraceLogger>();
			container.RegisterType<IGameDefinitionAccess, GameDefinitionAccess>();
			container.RegisterType<IGameSessionAccess, GameSessionAccess>();
			container.RegisterType<IPlayerAccess, PlayerAccess>();
			container.RegisterType<ITileAccess, TileAccess>();
			container.RegisterType<IGameBoardEngine, GameBoardEngine>();
			container.RegisterType<IGamePlayEngine, GamePlayEngine>();
			container.RegisterType<IValidationEngine, ValidationEngine>();

			container.RegisterType<IGameManager, GameManager>();

			var gameManager = container.Resolve<IGameManager>();
			var client = new ConsoleClient(gameManager);
			await client.Run();

		}

	}

}