namespace Gamer;

public abstract class BoardGame(int rows, int cols, char player1, char player2)
{

    protected readonly char player1 = player1;
    protected readonly char player2 = player2;

    protected char currentPlayer = player1;

    public char[,] Board { get; init; } = new char[rows, cols];

    public abstract void Start();
    public abstract void InitializeBoard();
    public abstract void PrintBoard();
    public abstract void PlayerMove();
    public abstract bool CheckWin();
    public abstract bool IsBoardFull();
    protected abstract bool IsValidMove(int row, int col);

    protected void SwitchPlayer()
    {
        currentPlayer = currentPlayer == player1 ? player2 : player1;
    }

    public void MovePiece(int row, int col, char player)
    {
        if (IsValidMove(row, col))
        {
            Board[row, col] = player;
        }
        else
        {
            throw new InvalidOperationException("Invalid move.");
        }
    }

}