using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Gamer.Access.Tile.Interface;
using Gamer.Framework;
using Gamer.Utility.ServiceMessaging;
using Microsoft.Extensions.Logging;

namespace Gamer.Access.Tile.Service
{

	public class TileAccess : ServiceBase, ITileAccess
	{

		private static readonly HashSet<Interface.Tile> cache;

		static TileAccess()
		{
			cache = new HashSet<Interface.Tile>();
		}

		public TileAccess(ILogger logger)
			:base(logger)
		{

		}

		public async Task<CreateTilesResponse> CreateTilesAsync(CreateTilesRequest request)
		{

			Contract.Assert(request.Tiles.Any(), "No tiles input to create.");
			var response = ServiceMessageFactory<CreateTilesResponse>.CreateFrom(request);

			try
			{
				var firstTile = request.Tiles.First();
				foreach (var tile in request.Tiles)
				{
					if (cache.FirstOrDefault(i => i.Id == tile.Id) == null)
					{
						cache.Add(tile);
					}
				}
				response.Tiles = cache.Where(i => i.GameSessionId == firstTile.GameSessionId).ToArray();
			}
			catch (Exception ex)
			{
				response.Errors = "Unable to add tiles.";
			}
			return await Task.FromResult(response);

		}

		public async Task<FindTilesResponse> FindTilesAsync(FindTilesRequest request)
		{

			var response = ServiceMessageFactory<FindTilesResponse>.CreateFrom(request);
			response.Tiles = cache.Where(request.Filter).ToArray();
			return await Task.FromResult(response);

		}

		public async Task<UpdateTileResponse> UpdateTileAsync(UpdateTileRequest request)
		{

			var response = ServiceMessageFactory<UpdateTileResponse>.CreateFrom(request);
			var cachedTile = cache.FirstOrDefault(i => i.Id == request.Tile.Id);
			if (cachedTile != null)
			{
				cachedTile.PlayerId = request.Tile.PlayerId;
				response.Tile = cachedTile;
			}
			else
			{
				response.Errors = "Unable to find the tile to update.";
			}
			return await Task.FromResult(response);

		}

	}

}