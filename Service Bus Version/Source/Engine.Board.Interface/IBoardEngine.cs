using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Engine.Board.Interface
{

	[ServiceContract]
	public interface IBoardEngine
	{

		[OperationContract]
		Task<Board> CreateBoard(Guid gameId);

		[OperationContract]
		Task<bool> PlaceTile(Guid boardId, string column, int row, string gamePiece);

		[OperationContract]
		Task<bool> IsGamePlayable(Guid boardId);

		[OperationContract]
		Task<bool> IsPositionAvailable(Guid boardId, string column, int row);

		[OperationContract]
		Task<bool> DestroyBoard(Guid gameId);

	}

}