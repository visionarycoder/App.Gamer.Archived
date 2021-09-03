using System;

namespace Access.Board.Interface
{

	public interface IBoardAccess
	{

		IGameTile[] ShowTiles(Guid boardId);
		void UpdateTiles(IGameTile[] tiles);

	}


}