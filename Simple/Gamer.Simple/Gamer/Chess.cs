namespace Gamer;

public class Chess() : BoardGame(8, 8, 'W', 'B')
{
    public override void Start()
    {
        InitializeBoard();
        while (true)
        {
            PrintBoard();
            PlayerMove();
            if (CheckWin())
            {
                PrintBoard();
                Console.WriteLine($"Player {currentPlayer} wins!");
                break;
            }
            SwitchPlayer();
        }
    }

    public override void InitializeBoard()
    {
        var initialSetup = new[]
        {
            "RNBQKBNR",
            "PPPPPPPP",
            "        ",
            "        ",
            "        ",
            "        ",
            "pppppppp",
            "rnbqkbnr"
        };

        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                Board[i, j] = initialSetup[i][j];
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
            if (i < 7) Console.WriteLine("-----------------");
        }
    }

    public override void PlayerMove()
    {
        while (true)
        {
            Console.WriteLine($"Player {currentPlayer}, enter your move (start row, start column, end row, end column): ");
            if (int.TryParse(Console.ReadLine(), out var startRow) &&
                int.TryParse(Console.ReadLine(), out var startCol) &&
                int.TryParse(Console.ReadLine(), out var endRow) &&
                int.TryParse(Console.ReadLine(), out var endCol) &&
                IsValidMove(startRow, startCol, endRow, endCol))
            {
                Board[endRow, endCol] = Board[startRow, startCol];
                Board[startRow, startCol] = ' ';
                break;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
            }
        }
    }

    public bool IsValidMove(int startRow, int startCol, int endRow, int endCol)
    {
        // Check if the move is within bounds
        if (startRow < 0 || startRow >= 8 || 
            startCol < 0 || startCol >= 8 ||
            endRow < 0 || endRow >= 8 || 
            endCol < 0 || endCol >= 8)
        {
            return false;
        }

        // Check if the start and end positions are valid
        var piece = Board[startRow, startCol];
        if (piece == ' ' || (char.IsUpper(piece) && currentPlayer == 'B') || (char.IsLower(piece) && currentPlayer == 'W'))
        {
            return false;
        }

        // Check if the destination is not occupied by the current player's piece
        if ((char.IsUpper(Board[endRow, endCol]) && currentPlayer == 'W') || (char.IsLower(Board[endRow, endCol]) && currentPlayer == 'B'))
        {
            return false;
        }

        // Determine the type of piece and validate the move accordingly
        return char.ToLower(piece) switch
        {
            'p' => // Pawn
                IsValidPawnMove(startRow, startCol, endRow, endCol, piece),
            'r' => // Rook
                IsValidRookMove(startRow, startCol, endRow, endCol),
            'n' => // Knight
                IsValidKnightMove(startRow, startCol, endRow, endCol),
            'b' => // Bishop
                IsValidBishopMove(startRow, startCol, endRow, endCol),
            'q' => // Queen
                IsValidQueenMove(startRow, startCol, endRow, endCol),
            'k' => // King
                IsValidKingMove(startRow, startCol, endRow, endCol),
            _ => false
        };
    }

    private bool IsValidPawnMove(int startRow, int startCol, int endRow, int endCol, char piece)
    {
        var direction = char.IsUpper(piece) ? -1 : 1;
        if (startCol == endCol)
        {
            if (Board[endRow, endCol] == ' ' && (endRow - startRow == direction || (startRow == (direction == 1 ? 1 : 6) && endRow - startRow == 2 * direction && Board[startRow + direction, startCol] == ' ')))
            {
                return true;
            }
        }
        else if (Math.Abs(startCol - endCol) == 1 && endRow - startRow == direction && Board[endRow, endCol] != ' ' && char.IsUpper(Board[endRow, endCol]) != char.IsUpper(piece))
        {
            return true;
        }
        return false;
    }

    private bool IsValidRookMove(int startRow, int startCol, int endRow, int endCol)
    {
        if (startRow != endRow && startCol != endCol) return false;
        var rowStep = startRow == endRow ? 0 : (endRow > startRow ? 1 : -1);
        var colStep = startCol == endCol ? 0 : (endCol > startCol ? 1 : -1);
        for (int i = startRow + rowStep, j = startCol + colStep; i != endRow || j != endCol; i += rowStep, j += colStep)
        {
            if (Board[i, j] != ' ') return false;
        }
        return true;
    }

    private bool IsValidKnightMove(int startRow, int startCol, int endRow, int endCol)
    {
        var rowDiff = Math.Abs(startRow - endRow);
        var colDiff = Math.Abs(startCol - endCol);
        return (rowDiff == 2 && colDiff == 1) || (rowDiff == 1 && colDiff == 2);
    }

    private bool IsValidBishopMove(int startRow, int startCol, int endRow, int endCol)
    {
        if (Math.Abs(startRow - endRow) != Math.Abs(startCol - endCol)) return false;
        var rowStep = endRow > startRow ? 1 : -1;
        var colStep = endCol > startCol ? 1 : -1;
        for (int i = startRow + rowStep, j = startCol + colStep; i != endRow; i += rowStep, j += colStep)
        {
            if (Board[i, j] != ' ') return false;
        }
        return true;
    }

    private bool IsValidQueenMove(int startRow, int startCol, int endRow, int endCol)
    {
        return IsValidRookMove(startRow, startCol, endRow, endCol) || IsValidBishopMove(startRow, startCol, endRow, endCol);
    }

    private bool IsValidKingMove(int startRow, int startCol, int endRow, int endCol)
    {
        var rowDiff = Math.Abs(startRow - endRow);
        var colDiff = Math.Abs(startCol - endCol);
        return rowDiff <= 1 && colDiff <= 1;
    }
    
    public override bool CheckWin()
    {
        bool whiteKingExists = false, blackKingExists = false;
        foreach (var cell in Board)
        {
            switch (cell)
            {
                case 'K':
                    whiteKingExists = true;
                    break;
                case 'k':
                    blackKingExists = true;
                    break;
            }
        }
        return !whiteKingExists || !blackKingExists;
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