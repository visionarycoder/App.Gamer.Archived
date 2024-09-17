using System;

namespace Client.Content.ConsoleApp
{
    public class GameUI
    {
        private GameLogic gameLogic;
        private GameRepository gameRepository;

        public GameUI()
        {
            gameLogic = new GameLogic();
            gameRepository = new GameRepository();
        }

        public void Start()
        {
            gameLogic.InitializePlayers("Player 1", "Player 2");
            gameLogic.SetGameState(gameRepository.LoadGame());

            int move;
            bool gameWon = false;

            do
            {
                Console.Clear();
                PrintBoard();
                Console.WriteLine($"{gameLogic.Players[gameLogic.GameState.CurrentPlayerIndex].Name} ({gameLogic.Players[gameLogic.GameState.CurrentPlayerIndex].Symbol}), enter your move (1-9): ");
                string input = Console.ReadLine() ?? string.Empty;

                if (int.TryParse(input, out move) && move >= 1 && move <= 9)
                {
                    if (gameLogic.MakeMove(move))
                    {
                        gameWon = gameLogic.CheckWin();
                        gameRepository.SaveGame(gameLogic.GameState);
                    }
                    else
                    {
                        Console.WriteLine("Invalid move! Try again.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number between 1 and 9.");
                    Console.ReadKey();
                }
            } while (!gameWon && !gameLogic.IsBoardFull());

            Console.Clear();
            PrintBoard();

            if (gameWon)
            {
                Console.WriteLine($"{gameLogic.Players[(gameLogic.GameState.CurrentPlayerIndex + 1) % 2].Name} wins!");
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }

            // Clear the saved game after it ends
            gameRepository.DeleteGame(gameLogic.GameState);
        }

        private void PrintBoard()
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {gameLogic.GameState.Board[0]}  |  {gameLogic.GameState.Board[1]}  |  {gameLogic.GameState.Board[2]}  ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {gameLogic.GameState.Board[3]}  |  {gameLogic.GameState.Board[4]}  |  {gameLogic.GameState.Board[5]}  ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {gameLogic.GameState.Board[6]}  |  {gameLogic.GameState.Board[7]}  |  {gameLogic.GameState.Board[8]}  ");
            Console.WriteLine("     |     |      ");
        }
    }
}
