using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Gamer.StandAlone.Components.Access.GameDefinition;
using Gamer.StandAlone.Components.Access.GameSessions;
using Gamer.StandAlone.Components.Access.Player;
using Gamer.StandAlone.Components.Access.Tile;
using Gamer.StandAlone.Components.Engine.AutoPlay;
using Gamer.StandAlone.Components.Engine.GamePlay;
using Gamer.StandAlone.Components.Engine.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gamer.StandAlone.Components.Manager.Game
{

    public class GameManager : IGameManager
    {

        private readonly IGameDefinitionAccess gameDefinitionAccess;
        private readonly IGameSessionAccess gameSessionAccess;
        private readonly IPlayerAccess playerAccess;
        private readonly ITileAccess tileAccess;

        private readonly IAutoPlayEngine autoPlayEngine;
        private readonly IGamePlayEngine gamePlayEngine;
        private readonly IValidationEngine validationEngine;

        public GameManager(IHost host)
        : this(
            ActivatorUtilities.CreateInstance<IGameDefinitionAccess>(host.Services)
            , ActivatorUtilities.CreateInstance<IGameSessionAccess>(host.Services)
            , ActivatorUtilities.CreateInstance<IPlayerAccess>(host.Services)
            , ActivatorUtilities.CreateInstance<ITileAccess>(host.Services)
            , ActivatorUtilities.CreateInstance<IAutoPlayEngine>(host.Services)
            , ActivatorUtilities.CreateInstance<IGamePlayEngine>(host.Services)
            , ActivatorUtilities.CreateInstance<IValidationEngine>(host.Services)
            )
        {

        }
        public GameManager(
            IGameDefinitionAccess gameDefinitionAccess
            , IGameSessionAccess gameSessionAccess
            , IPlayerAccess playerAccess
            , ITileAccess tileAccess
            , IAutoPlayEngine autoPlayEngine
            , IGamePlayEngine gamePlayEngine
            , IValidationEngine validationEngine)
        {
            this.gameDefinitionAccess = gameDefinitionAccess;
            this.gameSessionAccess = gameSessionAccess;
            this.playerAccess = playerAccess;
            this.tileAccess = tileAccess;
            this.autoPlayEngine = autoPlayEngine;
            this.gamePlayEngine = gamePlayEngine;
            this.validationEngine = validationEngine;
        }

        public async Task<GameDefinition[]> GetGames()
        {
            var gameDefinitions = await gameDefinitionAccess.GetGameDefinitions();
            return gameDefinitions;
        }

        public async Task<Tile> GetAutoPlayTurnInput(Guid gameSessionId, Guid playerId)
        {
            return await autoPlayEngine.PlayTurn(gameSessionId, playerId);
        }

        public async Task<ValidationResult> ValidateInput(Guid gameSessionId, string address)
        {
            var results = await validationEngine.ValidateUserInput(gameSessionId, address);
            return results;
        }

        public async Task ApplyTurn(Tile tile)
        {
            await tileAccess.UpdateTiles(new[] {tile});
        }

        public async Task<bool> CheckGameStatus(Guid gameSessionId)
        {
            var isPlayable = await gamePlayEngine.IsPlayable(gameSessionId);
            return isPlayable;
        }
        
        public async Task Play(Guid gameDefinition)
        {

            //StartGame();

            //// How many players?
            //var idx = OFFSET;
            //var items = AllowedNumberOfPlayers.Select(i => i.ToString()).ToList();
            //Console.WriteLine("How many players? ");
            //foreach (var item in items)
            //    Console.WriteLine($"[{idx++}] {item}");

            //int playerCount;
            //do
            //{
            //    playerCount = ConsoleHelper.GetIntegerInput() - OFFSET;
            //    if (AllowedNumberOfPlayers.Contains(playerCount))
            //        break;
            //} while (true);

            //Console.WriteLine(playerCount + " player(s) selected.");
            //SetPlayerCount(playerCount);

            //while (await gamePlayEngine.IsPlayable())
            //{

            //    var board = boardAccess.GetBoard()

            //    Console.WriteLine(board);
            //    Console.WriteLine(players.CurrentPlayer.Name);

            //    if (players.CurrentPlayer.IsMachine)
            //    {
            //        var address = gamePlayEngine.AutoPlay();
            //        ApplyTurn(address);
            //    }
            //    else
            //    {
            //        var address = "";
            //        while (string.IsNullOrWhiteSpace(address))
            //            address = RequestCellAddress();
            //        ApplyTurn(address);
            //    }

            //    players.IncrementPlayer();

            //}

            //EndGame();
            //ConsoleHelper.ShowExit();
        }

        //private void SetPlayerCount(int playerCount)
        //{

        //    TicTacToePlayer player1;
        //    TicTacToePlayer player2;

        //    switch (playerCount)
        //    {
        //        case 0:
        //            player1 = new TicTacToePlayer("Player 1", "X", true);
        //            player2 = new TicTacToePlayer("Player 2", "O", true);
        //            break;
        //        case 1:
        //            player1 = new TicTacToePlayer("Player 1", "X");
        //            player2 = new TicTacToePlayer("Player 2", "O", true);
        //            break;
        //        case 2:
        //            player1 = new TicTacToePlayer("Player 1", "X");
        //            player2 = new TicTacToePlayer("Player 2", "O");
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }

        //    players = new TicTacToePlayers(player1, player2);

        //}

        //private void ApplyTurn(string address)
        //{

        //    var cell = board.Cells.FirstOrDefault(i => i.Address == address);

        //    if (cell == null)
        //        throw new ArgumentException("Address does not exist");

        //    if (!string.IsNullOrWhiteSpace(cell.GamePiece))
        //        throw new ArgumentException("This cell has already been played.");

        //    cell.GamePiece = players.CurrentPlayer.GamePiece;

        //}

        //private string RequestCellAddress()
        //{

        //    do
        //    {
        //        Console.WriteLine("Please take a turn.");
        //        var address = Console.ReadLine()?.Trim().ToUpperInvariant();
        //        var validationResult = validationEngine.ValidateUserInput(address);
        //        if (validationResult == ValidationResult.Success)
        //        {
        //            if (gamePlayEngine.IsTileOpen(address))
        //                return address;
        //            validationResult = new ValidationResult("Tile has already been played.");
        //        }
        //        Console.WriteLine(validationResult.ErrorMessage);
        //    } while (true);

        //}

        //private void StartGame()
        //{
        //    Console.WriteLine("Tic-Tac-Toe");
        //}

        //private void EndGame()
        //{

        //    // Show board
        //    Console.WriteLine(board);

        //    // Declare winner
        //    var gamePiece = gamePlayEngine.FindWinnerGamePiece();
        //    var player = players.ByGamePiece(gamePiece);
        //    if (player == null)
        //        Console.WriteLine("It's a draw!");
        //    else
        //        Console.WriteLine(player.Name + " won!");

        //    Console.WriteLine("Thanks for playing.");

        //}

    }

}