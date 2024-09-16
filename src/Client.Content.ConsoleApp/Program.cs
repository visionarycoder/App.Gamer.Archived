namespace Client.Content.ConsoleApp
{
    class Program
    {
        static GameState gameState = new GameState();
        static Player[] players = new Player[2];

        static void Main(string[] args)
        {
            players[0] = new Player("Player 1", 'X');
            players[1] = new Player("Player 2", 'O');

            using (var db = new GameContext())
            {
                db.Database.EnsureCreated();
                LoadGame(db);

                int move;
                bool gameWon = false;

                do
                {
                    Console.Clear();
                    PrintBoard();
                    Console.WriteLine($"{players[gameState.CurrentPlayerIndex].Name} ({players[gameState.CurrentPlayerIndex].Symbol}), enter your move (1-9): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out move) && move >= 1 && move <= 9)
                    {
                        if (gameState.Board[move - 1] != 'X' && gameState.Board[move - 1] != 'O')
                        {
                            gameState.Board[move - 1] = players[gameState.CurrentPlayerIndex].Symbol;
                            gameWon = CheckWin();
                            gameState.CurrentPlayerIndex = (gameState.CurrentPlayerIndex + 1) % 2;
                            SaveGame(db);
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
                } while (!gameWon && !IsBoardFull());

                Console.Clear();
                PrintBoard();

                if (gameWon)
                {
                    Console.WriteLine($"{players[(gameState.CurrentPlayerIndex + 1) % 2].Name} wins!");
                }
                else
                {
                    Console.WriteLine("It's a draw!");
                }

                // Clear the saved game after it ends
                db.GameStates.Remove(gameState);
                db.SaveChanges();
            }
        }

        static void PrintBoard()
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {gameState.Board[0]}  |  {gameState.Board[1]}  |  {gameState.Board[2]}  ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {gameState.Board[3]}  |  {gameState.Board[4]}  |  {gameState.Board[5]}  ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {gameState.Board[6]}  |  {gameState.Board[7]}  |  {gameState.Board[8]}  ");
            Console.WriteLine("     |     |      ");
        }

        static bool CheckWin()
        {
            // Horizontal, Vertical & Diagonal Check
            return (gameState.Board[0] == gameState.Board[1] && gameState.Board[1] == gameState.Board[2]) ||
                   (gameState.Board[3] == gameState.Board[4] && gameState.Board[4] == gameState.Board[5]) ||
                   (gameState.Board[6] == gameState.Board[7] && gameState.Board[7] == gameState.Board[8]) ||
                   (gameState.Board[0] == gameState.Board[3] && gameState.Board[3] == gameState.Board[6]) ||
                   (gameState.Board[1] == gameState.Board[4] && gameState.Board[4] == gameState.Board[7]) ||
                   (gameState.Board[2] == gameState.Board[5] && gameState.Board[5] == gameState.Board[8]) ||
                   (gameState.Board[0] == gameState.Board[4] && gameState.Board[4] == gameState.Board[8]) ||
                   (gameState.Board[2] == gameState.Board[4] && gameState.Board[4] == gameState.Board[6]);
        }

        static bool IsBoardFull()
        {
            foreach (char c in gameState.Board)
            {
                if (c != 'X' && c != 'O')
                {
                    return false;
                }
            }
            return true;
        }

        static void SaveGame(GameContext db)
        {
            var existingGame = db.GameStates.FirstOrDefault();
            if (existingGame != null)
            {
                existingGame.Board = gameState.Board;
                existingGame.CurrentPlayerIndex = gameState.CurrentPlayerIndex;
            }
            else
            {
                db.GameStates.Add(gameState);
            }
            db.SaveChanges();
        }

        static void LoadGame(GameContext db)
        {
            var savedGame = db.GameStates.FirstOrDefault();
            if (savedGame != null)
            {
                gameState = savedGame;
            }
            else
            {
                gameState = new GameState
                {
                    Board = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' },
                    CurrentPlayerIndex = 0
                };
            }
        }
    }
}
