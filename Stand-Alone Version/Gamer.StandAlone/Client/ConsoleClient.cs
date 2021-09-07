using System;
using System.Threading.Tasks;
using Gamer.StandAlone.Components.Manager.Game;
using Gamer.StandAlone.Framework.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gamer.StandAlone.Client
{
    
    public class ConsoleClient
    {

        private readonly IGameManager gameManager;

        public ConsoleClient(IHost host)
        {
            gameManager = ActivatorUtilities.CreateInstance<IGameManager>(host.Services);
        }

        public void Play()
        {

            //
            // Select Game
            // Play Game - Loop
            //   Get Turn Input
            //   Validate Input
            //   Apply Input
            //   Check Game Status
            //   If Finished
            //     Exit Loop
            // Declare Winner
            //

            Task.Factory.StartNew(async () =>
            {
                var gameDefinitions = await gameManager.GetGames();
                var gameSelected = true;
                do
                {
                    Console.WriteLine("Select a game to play.");
                    var idx = 1;
                    foreach (var gameDefinition in gameDefinitions)
                    {
                        Console.WriteLine($"[{idx++}] {gameDefinition.Name}");
                    }
                    var input = ConsoleHelper.GetIntegerInput();

                } while (! gameSelected);

            });



        }

    }

}
