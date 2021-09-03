using System;
using System.IO;
using System.Net;
using Engine.Board.Interface;
using Engine.Board.Service;
using Engine.Player.Interface;
using Engine.Player.Service;
using Manager.GamePlay.Interface;
using Manager.GamePlay.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client.ConsoleApp
{

    class Program
    {

        static void Main(string[] args)
        {

            var host = AppStart();
            var manager = ActivatorUtilities.CreateInstance<IGamePlayManager>(host.Services);

        }

        static IHost AppStart()
        {
            var builder = new ConfigurationBuilder();
            ConfigSetup(builder);

            var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IGamePlayManager, GamePlayManager>();
                    services.AddTransient<IBoardEngine, BoardEngine>();
                    services.AddTransient<IGamePlayEngine, GamePlayEngine>();
                    services.AddTransient<IPlayerEngine, PlayerEngine>();
                    services.AddTransient<IGameSessionAccess, GameSessionAccess>();
                    services.AddTransient<IPlayerAccess, PlayerAccess>();
                    services.AddTransient<ITileAccess, TileAccess>();
                })
                .Build();

            return host;
        }

        static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
