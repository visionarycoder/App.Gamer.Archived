using System;
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
using Gamer.Manager.Game.Interface;
using Gamer.Manager.Game.Service;
using Unity;

namespace Gamer.Client.ConsoleAppAsync
{
	class Program
	{
		static void Main(string[] args)
		{

			var container = new UnityContainer();
			container.RegisterType<IGameDefinitionAccessAsync, GameDefinitionAccess>();
			container.RegisterType<IGameSessionAccessAsync, GameSessionAccess>();
			container.RegisterType<IPlayerAccessAsync, PlayerAccess>();
			container.RegisterType<ITileAccessAsync, TileAccess>();
			container.RegisterType<IGameBoardEngineAsync, GameBoardEngine>();
			container.RegisterType<IGamePlayEngineAsync, GamePlayEngine>();
			container.RegisterType<IValidationEngineAsync, ValidationEngine>();

			container.RegisterType<IGameManagerAsync, GameManager>();

			var gameManager = container.Resolve<IGameManager>();
			var client = new ConsoleClient(gameManager);
			await client.Run();

		}

	}

}
