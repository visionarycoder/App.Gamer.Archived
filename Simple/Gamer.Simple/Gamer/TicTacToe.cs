namespace Gamer;

public class TicTacToe : BoardGame
{
    private int numberOfPlayers;

    public TicTacToe() : base(3, 3, 'X', 'O') { }

    public override void Start()
    {
        InitializeBoard();
        RequestNumberOfPlayers();

        while (true)
        {
            PrintBoard();
            switch (numberOfPlayers)
            {
                case 0:
                    AutomatedPlayerMove();
                    break;
                case 1 when currentPlayer == 'X':
                    PlayerMove();
                    break;
                case 1:
                    AutomatedPlayerMove();
                    break;
                default:
                    PlayerMove();
                    break;
            }

            if (CheckWin())
            {
                PrintBoard();
                Console.WriteLine($"Player {currentPlayer} wins!");
                break;
            }
            if (IsBoardFull())
            {
                PrintBoard();
                Console.WriteLine("It's a draw!");
                break;
            }
            SwitchPlayer();
        }
    }

    private void RequestNumberOfPlayers()
    {
        while (true)
        {
            Console.WriteLine("Enter the number of players (0, 1, or 2): ");
            if (int.TryParse(Console.ReadLine(), out numberOfPlayers) && numberOfPlayers is >= 0 and <= 2)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 0, 1, or 2.");
            }
        }
    }

    public override void InitializeBoard()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Board[i, j] = ' ';
            }
        }
    }

    public override void PrintBoard()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Console.Write(Board[i, j]);
                if (j < 2) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("-----");
        }
    }

    public override void PlayerMove()
    {
        while (true)
        {
            Console.WriteLine($"Player {currentPlayer}, enter your move (row and column): ");
            if (int.TryParse(Console.ReadLine(), out var row) &&
                int.TryParse(Console.ReadLine(), out var col) &&
                IsValidMove(row, col))
            {
                MovePiece(row, col, currentPlayer);
                break;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }
    }

    private void AutomatedPlayerMove()
    {
        var bestScore = int.MinValue;
        var bestRow = -1;
        var bestCol = -1;

        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (Board[i, j] == ' ')
                {
                    MovePiece(i, j, currentPlayer);
                    var score = Minimax(0, false);
                    MovePiece(i, j, ' ');
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestRow = i;
                        bestCol = j;
                    }
                }
            }
        }

        MovePiece(bestRow, bestCol, currentPlayer);
    }

    private int Minimax(int depth, bool isMaximizing)
    {
        if (CheckWin())
        {
            return isMaximizing ? -1 : 1;
        }
        if (IsBoardFull())
        {
            return 0;
        }

        if (isMaximizing)
        {
            var bestScore = int.MinValue;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Board[i, j] != ' ')
                        continue;
                    MovePiece(i, j, currentPlayer);
                    var score = Minimax(depth + 1, false);
                    MovePiece(i, j, ' ');
                    bestScore = Math.Max(score, bestScore);
                }
            }
            return bestScore;
        }
        else
        {
            var bestScore = int.MaxValue;
            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (Board[i, j] != ' ')
                        continue;

                    MovePiece(i, j, currentPlayer == 'X' ? 'O' : 'X');
                    var score = Minimax(depth + 1, true);
                    MovePiece(i, j, ' ');
                    bestScore = Math.Min(score, bestScore);
                }
            }
            return bestScore;
        }
    }

    public override bool CheckWin()
    {
        for (var i = 0; i < 3; i++)
        {
            if (Board[i, 0] == currentPlayer && Board[i, 1] == currentPlayer && Board[i, 2] == currentPlayer)
                return true;
            if (Board[0, i] == currentPlayer && Board[1, i] == currentPlayer && Board[2, i] == currentPlayer)
                return true;
        }
        if (Board[0, 0] == currentPlayer && Board[1, 1] == currentPlayer && Board[2, 2] == currentPlayer)
            return true;
        if (Board[0, 2] == currentPlayer && Board[1, 1] == currentPlayer && Board[2, 0] == currentPlayer)
            return true;
        return false;
    }

    public override bool IsBoardFull()
    {
        foreach (var cell in Board)
        {
            if (cell == ' ') return false;
        }
        return true;
    }

    protected override bool IsValidMove(int row, int col)
    {
        return row is >= 0 and < 3 && col is >= 0 and < 3 && Board[row, col] == ' ';
    }
}
