using System.IO;
using Gamer.StandAlone.Client;
using Gamer.StandAlone.Components.Access.GameDefinition;
using Gamer.StandAlone.Components.Access.GameSessions;
using Gamer.StandAlone.Components.Access.Player;
using Gamer.StandAlone.Components.Access.Tile;
using Gamer.StandAlone.Components.Engine.GamePlay;
using Gamer.StandAlone.Components.Engine.Validation;
using Gamer.StandAlone.Components.Manager;
using Gamer.StandAlone.Components.Manager.Game;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gamer.StandAlone
{

    class Hosting
    {

        static void Main(string[] args)
        {
            
            var host = AppStart();
            var client = new ConsoleClient(host);
            client.Play();

        }

        static void ConfigurationSetup(IConfigurationBuilder builder)
        {
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        static IHost AppStart()
        {
            
            var builder = new ConfigurationBuilder();
            ConfigurationSetup(builder);

            // Add Logging

            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {

                    services.AddTransient<IGameDefinitionAccess, GameDefinitionAccess>();
                    services.AddTransient<IGameSessionAccess, GameSessionAccess>();
                    services.AddTransient<IPlayerAccess, PlayerAccess>();
                    services.AddTransient<ITileAccess, TileAccess>();

                    services.AddTransient<IGamePlayEngine, GamePlayEngine>();
                    services.AddTransient<IValidationEngine, ValidationEngine>();

                    services.AddTransient<IGameManager, GameManager>();

                })
                .Build();

            return host;

        }

    }

}
