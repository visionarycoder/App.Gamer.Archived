using System;
using System.Data;
using System.Threading.Tasks;

namespace Gamer.Component.Engine.GameBoard
{
	public interface IGameBoardEngine
	{
		Task<DataTable> GetBoard(Guid gameSession);
	}
}