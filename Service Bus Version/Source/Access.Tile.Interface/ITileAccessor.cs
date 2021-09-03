using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Gamer.Access.Tile.Interface
{

	[ServiceContract]
	public interface ITileAccessor
	{

		[OperationContract]
		Task<bool> CreateTiles(Tile[] tiles);

		[OperationContract]
		Task<bool> DeleteTiles(Guid tiles);

		[OperationContract]
		Task<Tile> GetTile(Guid boardId, string column, int row);

		[OperationContract]
		Task<Tile[]> GetTiles(Guid boardId);

		[OperationContract]
		Task<bool> UpdateTile(Guid boardId, string column, int row, string gamePiece);

		[OperationContract]
		Task<bool> UpdateTiles(Tile[] tiles);

	}

}