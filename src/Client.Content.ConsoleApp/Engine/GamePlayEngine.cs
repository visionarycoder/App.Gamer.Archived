using Client.Content.ConsoleApp.Resources.Data.GamerDB.Models;

namespace Client.Content.ConsoleApp.Engine
{
    public class GamePlayEngine
    {
        private readonly RulesEngine.RulesEngine rulesEngine;

        public GamePlayEngine(RulesEngine.RulesEngine rulesEngine)
        {
            this.rulesEngine = rulesEngine;
        }

        public async Task<bool> MakeMoveAsync(Session gameState, Player[] players, int move)
        {
            if (gameState.Board.Cells[move - 1] == " ")
            {
                gameState.Board.Cells[move - 1] = players[gameState.CurrentPlayerIndex].Token.Token;
                gameState.CurrentPlayerIndex = (gameState.CurrentPlayerIndex + 1) % players.Length;

                // Apply business rules after making a move
                var inputs = new Dictionary<string, object>
                        {
                            { "input1", gameState.Board.Cells[0] },
                            { "input2", gameState.Board.Cells[1] },
                            { "input3", gameState.Board.Cells[2] },
                            { "input4", gameState.Board.Cells[3] },
                            { "input5", gameState.Board.Cells[4] },
                            { "input6", gameState.Board.Cells[5] },
                            { "input7", gameState.Board.Cells[6] },
                            { "input8", gameState.Board.Cells[7] },
                            { "input9", gameState.Board.Cells[8] }
                        };

                var result = await rulesEngine.ExecuteAllRulesAsync("TicTacToeRules", inputs);
                foreach (var ruleResult in result)
                {
                    if (ruleResult.IsSuccess && ruleResult.Rule.RuleName == "InvalidMoveRule")
                    {
                        // Revert the move if it's invalid
                        gameState.Board.Cells[move - 1] = " ";
                        gameState.CurrentPlayerIndex = (gameState.CurrentPlayerIndex + 1) % players.Length;
                        return false;
                    }
                }

                return true;
            }
            return false;
        }

        public async Task<bool> CheckWinConditionAsync(Session gameState)
        {
            var inputs = new Dictionary<string, object>
                    {
                        { "input1", gameState.Board.Cells[0] },
                        { "input2", gameState.Board.Cells[1] },
                        { "input3", gameState.Board.Cells[2] },
                        { "input4", gameState.Board.Cells[3] },
                        { "input5", gameState.Board.Cells[4] },
                        { "input6", gameState.Board.Cells[5] },
                        { "input7", gameState.Board.Cells[6] },
                        { "input8", gameState.Board.Cells[7] },
                        { "input9", gameState.Board.Cells[8] }
                    };

            var result = await rulesEngine.ExecuteAllRulesAsync("TicTacToeRules", inputs);
            foreach (var ruleResult in result)
            {
                if (ruleResult.IsSuccess && ruleResult.Rule.RuleName == "WinningConditionRule")
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsBoardFull(Session gameState)
        {
            return gameState.Board.Cells.All(cell => cell == "X" || cell == "O");
        }
    }
}
