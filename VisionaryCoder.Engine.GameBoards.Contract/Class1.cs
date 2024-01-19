using System.Data.Common;
using VisionaryCoder.ifx;

namespace VisionaryCoder.Engine.GameBoards.Contract
{

    public interface IGameBoardEngine
    {
        Task<GameBoard> GetGameBoardEngineAsync(Identifier gameBoardIdentifier);
    }

    public abstract class GameBoard
    {

        public Identifier Id { get; set; }
        public string Name { get; set; }
        public bool IsPlayable { get; }

    }
    
}