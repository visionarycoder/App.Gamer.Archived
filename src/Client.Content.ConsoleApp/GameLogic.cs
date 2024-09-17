namespace Client.Content.ConsoleApp
{
    public class GameLogic
    {
        public GameState GameState { get; private set; }
        public Player[] Players { get; private set; }

        public GameLogic()
        {
            GameState = new GameState();
            Players = new Player[2];
        }

        public void InitializePlayers(string player1Name, string player2Name)
        {
            Players[0] = new Player(player1Name, 'X');
            Players[1] = new Player(player2Name, 'O');
        }

        public void SetGameState(GameState gameState)
        {
            GameState = gameState;
        }

        public bool MakeMove(int move)
        {
            if (GameState.Board[move - 1] != 'X' && GameState.Board[move - 1] != 'O')
            {
                GameState.Board[move - 1] = Players[GameState.CurrentPlayerIndex].Symbol;
                GameState.CurrentPlayerIndex = (GameState.CurrentPlayerIndex + 1) % 2;
                return true;
            }
            return false;
        }

        public bool CheckWin()
        {
            // Horizontal, Vertical & Diagonal Check
            return (GameState.Board[0] == GameState.Board[1] && GameState.Board[1] == GameState.Board[2]) ||
                   (GameState.Board[3] == GameState.Board[4] && GameState.Board[4] == GameState.Board[5]) ||
                   (GameState.Board[6] == GameState.Board[7] && GameState.Board[7] == GameState.Board[8]) ||
                   (GameState.Board[0] == GameState.Board[3] && GameState.Board[3] == GameState.Board[6]) ||
                   (GameState.Board[1] == GameState.Board[4] && GameState.Board[4] == GameState.Board[7]) ||
                   (GameState.Board[2] == GameState.Board[5] && GameState.Board[5] == GameState.Board[8]) ||
                   (GameState.Board[0] == GameState.Board[4] && GameState.Board[4] == GameState.Board[8]) ||
                   (GameState.Board[2] == GameState.Board[4] && GameState.Board[4] == GameState.Board[6]);
        }

        public bool IsBoardFull()
        {
            foreach (char c in GameState.Board)
            {
                if (c != 'X' && c != 'O')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
