using System.Threading.Tasks;
using Gamer.Client;
using Gamer.Component.Access.GameDefinition;
using Gamer.Component.Access.GameSession;
using Gamer.Component.Access.Player;
using Gamer.Component.Access.Tile;
using Gamer.Component.Engine.GameBoard;
using Gamer.Component.Engine.GamePlay;
using Gamer.Component.Engine.Validation;
using Gamer.Component.Manager.Game;
using Unity;

namespace Gamer
{

	class Hosting
	{

		static async Task Main(string[] args)
		{

			var container = Initialize();
			var gameManager = container.Resolve<IGameManager>();
			await new ConsoleClient().Run(gameManager);

		}

		private static UnityContainer Initialize()
		{

			var container = new UnityContainer();

			container.RegisterType<IGameDefinitionAccess, GameDefinitionAccess>();
			container.RegisterType<IGameSessionAccess, GameSessionAccess>();
			container.RegisterType<IPlayerAccess, PlayerAccess>();
			container.RegisterType<ITileAccess, TileAccess>();

			container.RegisterType<IGameBoardEngine, GameBoardEngine>();
			container.RegisterType<IGamePlayEngine, GamePlayEngine>();
			container.RegisterType<IValidationEngine, ValidationEngine>();

			container.RegisterType<IGameManager, GameManager>();

			return container;

		}

	}

}
