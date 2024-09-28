using Client.Content.ConsoleApp.Managers;
using Client.Content.ConsoleApp.Resources.Data.GamerDB.Models;

namespace Client.Content.ConsoleApp.Client
{
    public class GameClient
    {
        private readonly ContentManager contentManager;

        public GameClient(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public async Task StartAsync()
        {
            var availableGames = contentManager.GetAvailableGames();
            if (availableGames == null || !availableGames.Any())
            {
                Console.WriteLine("No available games to play.");
                return;
            }

            Console.WriteLine("Available games:");
            for (int i = 0; i < availableGames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableGames[i].Name}");
            }

            Console.WriteLine("Select a game to play by entering the corresponding number:");
            string input = Console.ReadLine() ?? string.Empty;
            if (int.TryParse(input, out int selectedGameIndex) && selectedGameIndex >= 1 && selectedGameIndex <= availableGames.Count)
            {
                Console.WriteLine($"You selected: {availableGames[selectedGameIndex - 1].Name}");
                // Load the selected game and get its ID
                var session = contentManager.LoadGame(availableGames[selectedGameIndex - 1].Id);
                int sessionId = session.Id;

                int move;
                bool gameWon = false;

                do
                {
                    Console.Clear();
                    PrintBoard(session.Board);
                    Console.WriteLine($"{session.Players[session.CurrentPlayerIndex].Name} ({session.Players[session.CurrentPlayerIndex].Token.Token}), enter your move (1-9): ");
                    input = Console.ReadLine() ?? string.Empty;

                    if (int.TryParse(input, out move) && move >= 1 && move <= 9)
                    {
                        if (await contentManager.MakeMoveAsync(sessionId, move))
                        {
                            gameWon = await contentManager.CheckWinConditionAsync(sessionId);
                            contentManager.SaveGame(sessionId);
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
                } while (!gameWon && !contentManager.IsBoardFull(sessionId));

                Console.Clear();
                PrintBoard(session.Board);

                if (gameWon)
                {
                    Console.WriteLine($"{session.Players[(session.CurrentPlayerIndex + 1) % 2].Name} wins!");
                }
                else
                {
                    Console.WriteLine("It's a draw!");
                }

                // Clear the saved game after it ends
                contentManager.DeleteGame(sessionId);
            }
            else
            {
                Console.WriteLine("Invalid selection! Exiting...");
            }
        }

        private void PrintBoard(Board board)
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {board.Cells[0]}  |  {board.Cells[1]}  |  {board.Cells[2]}  ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {board.Cells[3]}  |  {board.Cells[4]}  |  {board.Cells[5]}  ");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {board.Cells[6]}  |  {board.Cells[7]}  |  {board.Cells[8]}  ");
            Console.WriteLine("     |     |      ");
        }
    }
}
