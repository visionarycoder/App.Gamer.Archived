using System;
using System.Data;
using System.Threading.Tasks;

namespace Gamer.Engine.GameBoard.Interface
{
	public interface IGameBoardEngine
	{
		Task<DataTable> GetBoard(Guid gameSession);
	}
}