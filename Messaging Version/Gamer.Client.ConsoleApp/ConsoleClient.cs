using Gamer.Framework.Helpers;
using Gamer.Manager.Game.Interface;
using Gamer.Utility.ServiceMessaging;

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Gamer.Client.ConsoleApp
{

    public class ConsoleClient
    {

        private readonly IGameManager gameManager;

        public ConsoleClient(IGameManager gameManager)
        {

            this.gameManager = gameManager;

        }

        public async Task Run()
        {

            //
            // Select Game
            // Initialize players
            // Run Game - Loop
            //   Get Turn Input
            //   ValidateAsync Input
            //   Apply Input
            //   Check Game Status
            //   If Finished
            //     Exit Loop
            // Declare Winner
            //

            // Setup game.
            // This becomes the root message for all that follow.  
            // All correlation ids will link back to this message.

            var (gameDefinition, request) = await SelectGame();
            var playerCount = await SelectPlayerCount(gameDefinition);

            var startGameRequest = ServiceMessageFactory<StartGameRequest>.CreateFrom(request);
            startGameRequest.GameDefinitionId = gameDefinition.Id;
            startGameRequest.PlayerCount = playerCount;

            // Start a game
            var gameSessionResponse = await gameManager.StartGameAsync(startGameRequest);

            Console.WriteLine($"Starting {gameDefinition.Name}");

            var boardRequest = ServiceMessageFactory<GetBoardRequest>.CreateFrom(request);
            boardRequest.GameSessionId = gameSessionResponse.GameSessionId;

            var boardResponse = await gameManager.GetBoardAsync(boardRequest);
            ConsoleHelper.ShowGameBoard(boardResponse.GameBoard);

            bool isGamePlayable;
            do
            {

                var currentPlayerRequest = ServiceMessageFactory<GetCurrentPlayerRequest>.CreateFrom(request);
                currentPlayerRequest.GameSessionId = gameSessionResponse.GameSessionId;
                var currentPlayerResponse = await gameManager.GetCurrentPlayerAsync(currentPlayerRequest);

                if (currentPlayerResponse.Player.IsMachine)
                {
                    var applyTurnRequest = ServiceMessageFactory<ApplyTurnRequest>.CreateFrom(request);
                    applyTurnRequest.PlayerId = currentPlayerResponse.Player.Id;
                    applyTurnRequest.GameSessionId = gameSessionResponse.GameSessionId;
                    var applyTurnResponse = await gameManager.ApplyTurnAsync(applyTurnRequest);
                    // ToDo: Log any errors.
                }
                else
                {
                    var inputIsInvalid = true;
                    do
                    {

                        Console.WriteLine($"{gameDefinition.TurnPrompt} {currentPlayerResponse.Player.Name} ({currentPlayerResponse.Player.GamePiece}).");
                        var userInput = ConsoleHelper.GetStringInput();
                        var confirmUsableAddressRequest = ServiceMessageFactory<ConfirmUsableAddressRequest>.CreateFrom(request);
                        confirmUsableAddressRequest.Address = userInput;
                        confirmUsableAddressRequest.GameSessionId = gameSessionResponse.GameSessionId;
                        var validationResultResponse = await gameManager.ConfirmUsableAddressAsync(confirmUsableAddressRequest);
                        if (validationResultResponse.ValidationResult == ValidationResult.Success)
                        {

                            var applyTurnRequest = ServiceMessageFactory<ApplyTurnRequest>.CreateFrom(request);
                            applyTurnRequest.PlayerId = currentPlayerResponse.Player.Id;
                            applyTurnRequest.GameSessionId = gameSessionResponse.GameSessionId;
                            applyTurnRequest.Address = userInput;
                            var applyTurnResponse = await gameManager.ApplyTurnAsync(applyTurnRequest);
                            // ToDo: Log any errors.
                            inputIsInvalid = false;
                        }
                        else
                        {
                            Console.WriteLine($"{validationResultResponse.ValidationResult.ErrorMessage}.  Please try again.");
                        }

                    } while (inputIsInvalid);
                }

                var isGamePlayableRequest = ServiceMessageFactory<IsGamePlayableRequest>.CreateFrom(request);
                isGamePlayableRequest.GameSessionId = gameSessionResponse.GameSessionId;
                var isGamePlayableResponse = await gameManager.IsGamePlayableAsync(isGamePlayableRequest);
                // ToDo: Log errors
                isGamePlayable = isGamePlayableResponse.IsPlayable;
            } while (isGamePlayable);

            boardRequest = ServiceMessageFactory<GetBoardRequest>.CreateFrom(request);
            boardRequest.GameSessionId = gameSessionResponse.GameSessionId;
            boardResponse = await gameManager.GetBoardAsync(boardRequest);
            ConsoleHelper.ShowGameBoard(boardResponse.GameBoard);

            var findWinnerRequest = ServiceMessageFactory<FindWinnerRequest>.CreateFrom(request);
            findWinnerRequest.GameSessionId = gameSessionResponse.GameSessionId;
            var findWinnerResponse = await gameManager.FindWinnerAsync(findWinnerRequest);
            ConsoleHelper.ShowWinner(findWinnerResponse.Player.Name, findWinnerResponse.Player.GamePiece);

            ConsoleHelper.ShowExit();

        }

        private async Task<Tuple<GameDefinition, IServiceMessage>> SelectGame()
        {

            var getGamesRequest = ServiceMessageFactory<GetGamesRequest>.Create();
            var getGamesResponse = await gameManager.GetGamesAsync(getGamesRequest);
            while (true)
            {
                Console.WriteLine("Select a game to play.");
                var idx = 1;
                foreach (var gameDefinition in getGamesResponse.GameDefinitions)
                {
                    Console.WriteLine($"[{idx++}] {gameDefinition.Name}");
                }

                var input = ConsoleHelper.GetIntegerInput();
                if (input > 0 && input <= getGamesResponse.GameDefinitions.Length)
                {
                    return new Tuple<GameDefinition, IServiceMessage>(getGamesResponse.GameDefinitions[input - 1], getGamesRequest);
                }
                Console.WriteLine($"Invalid input ({input}){Environment.NewLine}");
            }

        }

        private async Task<int> SelectPlayerCount(GameDefinition gameDefinition)
        {

            Console.WriteLine("How many players?");
            while (true)
            {
                for (var idx = gameDefinition.MinNumberOfPlayers; idx <= gameDefinition.MaxNumberOfPlayers; idx++)
                {
                    Console.WriteLine($"[{idx}] {idx} player{(idx == 1 ? "" : "s")}.");
                }
                var input = ConsoleHelper.GetIntegerInput();
                if (input >= gameDefinition.MinNumberOfPlayers && input <= gameDefinition.MaxNumberOfPlayers)
                {
                    return await Task.FromResult(input);
                }
                Console.WriteLine($"Invalid input ({input}){Environment.NewLine}");
            }

        }

    }

}
