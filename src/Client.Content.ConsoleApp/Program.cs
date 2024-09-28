using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Client.Content.ConsoleApp.Resources.Data.GamerDB;
using Client.Content.ConsoleApp.Resources.Data.GamerDB.Models;
using Client.Content.ConsoleApp.Client;
using Client.Content.ConsoleApp.Access;
using Client.Content.ConsoleApp.Engine;
using Client.Content.ConsoleApp.Managers;

namespace Client.Content.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var gameClient = host.Services.GetRequiredService<GameClient>();

            await gameClient.StartAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("DefaultConnection");
                    services.AddDbContext<GameContext>(options =>
                        options.UseSqlite(connectionString));

                    services.AddTransient<GamesAccess>();
                    services.AddTransient<GamePlayEngine>();
                    services.AddTransient<ContentManager>();
                    services.AddTransient<GameClient>();

                    // Initialize RulesEngine with default Tic-Tac-Toe rules
                    var rulesEngine = Initialization.InitializeRulesEngine();
                    services.AddSingleton(rulesEngine);

                    // Initialize Definition for Tic-Tac-Toe
                    var gameDefinition = Initialization.InitializeGameDefinition();
                    services.AddSingleton(gameDefinition);

                    // Register GameState and Players as singleton or scoped depending on your use case
                    services.AddSingleton(gameDefinition.Players);
                    services.AddSingleton(new Session());
                });
    }
}