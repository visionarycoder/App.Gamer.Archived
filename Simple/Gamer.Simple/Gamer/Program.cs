namespace Gamer;

class Program
{

    static void Main(string[] args)
    {
        
        Console.WriteLine("Choose a game to play: 1. Tic-Tac-Toe 2. Checkers 3. Chess");
        int.TryParse(Console.ReadLine(), out var choice);
        switch (choice)
        {
            case 1:
                {
                    new TicTacToe().Start();
                    break;
                }
            case 2:
                {
                    new Checkers().Start();
                    break;
                }
            case 3:
                {
                    new Chess().Start();
                    break;
                }
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
        Console.WriteLine("Game over.");
        Console.ReadLine();
    }

}