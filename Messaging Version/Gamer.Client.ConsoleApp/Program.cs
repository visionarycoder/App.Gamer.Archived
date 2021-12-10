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
using Gamer.Manager.Game.Interface;
using Gamer.Manager.Game.Service;
using Gamer.Utility.Logging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;


namespace Gamer.Client.ConsoleApp
{

    class Program
    {

        static async Task Main(string[] args)
        {

            Console.WriteLine(AppConstant.ApplicationName);

            var host = Host
                .CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    {

                        services
                            .AddSingleton(typeof(ILogger), typeof(CustomLogger))

                            .AddSingleton(typeof(IGameDefinitionAccess), typeof(GameDefinitionAccess))
                            .AddSingleton(typeof(IGameSessionAccess), typeof(GameSessionAccess))
                            .AddSingleton(typeof(IPlayerAccess), typeof(PlayerAccess))
                            .AddSingleton(typeof(ITileAccess), typeof(TileAccess))

                            .AddSingleton(typeof(IGameBoardEngine), typeof(GameBoardEngine))
                            .AddSingleton(typeof(IGamePlayEngine), typeof(GamePlayEngine))
                            .AddSingleton(typeof(IValidationEngine), typeof(ValidationEngine))

                            .AddSingleton(typeof(IGameManager), typeof(GameManager));

                    })
                .Build();

            var gameManager = host.Services.GetService<IGameManager>();
            var client = new ConsoleClient(gameManager);
            await client.Run();

        }

    }

}