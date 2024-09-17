namespace Gamer;

public class Checkers() : BoardGame(8, 8, 'B', 'W')
{

    private int numberOfPlayers;

    public override void Start()
    {
        InitializeBoard();
        RequestNumberOfPlayers();

        while (true)
        {
            PrintBoard();
            if (numberOfPlayers == 0)
            {
                AutomatedPlayerMove();
            }
            else if (numberOfPlayers == 1)
            {
                if (currentPlayer == 'B')
                {
                    PlayerMove();
                }
                else
                {
                    AutomatedPlayerMove();
                }
            }
            else
            {
                PlayerMove();
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
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                if ((i < 3 || i > 4) && (i + j) % 2 == 1)
                {
                    Board[i, j] = i < 3 ? 'B' : 'W';
                }
                else
                {
                    Board[i, j] = ' ';
                }
            }
        }
    }

    public override void PrintBoard()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                Console.Write(Board[i, j]);
                if (j < 7) Console.Write("|");
            }
            Console.WriteLine();
            if (i < 7) Console.WriteLine("----------------");
        }
    }

    public override void PlayerMove()
    {
        while (true)
        {
            Console.WriteLine($"Player {currentPlayer}, enter your move (fromRow fromCol toRow toCol): ");
            if (int.TryParse(Console.ReadLine(), out var fromRow) &&
                int.TryParse(Console.ReadLine(), out var fromCol) &&
                int.TryParse(Console.ReadLine(), out var toRow) &&
                int.TryParse(Console.ReadLine(), out var toCol) &&
                IsValidMove(fromRow, fromCol, toRow, toCol))
            {
                MovePiece(fromRow, fromCol, toRow, toCol);
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
        // Simple AI: make the first valid move found
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                if (Board[i, j] == currentPlayer)
                {
                    for (var di = -1; di <= 1; di += 2)
                    {
                        for (var dj = -1; dj <= 1; dj += 2)
                        {
                            var toRow = i + di;
                            var toCol = j + dj;
                            if (IsValidMove(i, j, toRow, toCol))
                            {
                                MovePiece(i, j, toRow, toCol);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }

    public bool IsValidMove(int fromRow, int fromCol, int toRow, int toCol)
    {
        if (toRow < 0 || toRow >= 8 || toCol < 0 || toCol >= 8 || Board[toRow, toCol] != ' ')
            return false;

        var piece = Board[fromRow, fromCol];
        if (piece == ' ' || piece != currentPlayer)
            return false;

        var rowDiff = toRow - fromRow;
        var colDiff = toCol - fromCol;

        if (Math.Abs(rowDiff) == 1 && Math.Abs(colDiff) == 1)
        {
            return true;
        }

        if (Math.Abs(rowDiff) == 2 && Math.Abs(colDiff) == 2)
        {
            var midRow = (fromRow + toRow) / 2;
            var midCol = (fromCol + toCol) / 2;
            if (Board[midRow, midCol] != ' ' && Board[midRow, midCol] != currentPlayer)
            {
                Board[midRow, midCol] = ' ';
                return true;
            }
        }

        return false;
    }

    public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
    {
        Board[toRow, toCol] = Board[fromRow, fromCol];
        Board[fromRow, fromCol] = ' ';
    }

    public override bool CheckWin()
    {
        var blackCount = 0;
        var whiteCount = 0;
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                if (Board[i, j] == 'B') blackCount++;
                if (Board[i, j] == 'W') whiteCount++;
            }
        }
        return blackCount == 0 || whiteCount == 0;
    }

    public override bool IsBoardFull()
    {
        foreach (var cell in Board)
        {
            if (cell == ' ') return false;
        }
        return true;
    }
}