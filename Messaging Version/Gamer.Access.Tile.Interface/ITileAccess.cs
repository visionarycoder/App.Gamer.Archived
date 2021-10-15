using System;
using System.Threading.Tasks;
using Gamer.Framework;

namespace Gamer.Access.Tile.Interface
{

	public interface ITileAccess : IServiceBase
	{

		Task<CreateTilesResponse> CreateTilesAsync(CreateTilesRequest request);
		Task<FindTilesResponse> FindTilesAsync(FindTilesRequest request);
		Task<UpdateTileResponse> UpdateTileAsync(UpdateTileRequest request);

	}

}