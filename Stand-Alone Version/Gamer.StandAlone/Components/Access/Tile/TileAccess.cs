using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gamer.StandAlone.Components.Access.Tile
{
    public class TileAccess : ITileAccess
    {

        private static readonly HashSet<Tile> cache = new HashSet<Tile>();

        public async Task<Tile[]> FindTiles(Guid gameSessionId)
        {
            
            var tiles = cache.Where(i => i.GameSessionId == gameSessionId).ToArray();
            return await Task.FromResult(tiles);

        }

        public async Task<bool> RemoveTiles(Guid gameSessionId)
        {
            
            var count = cache.RemoveWhere(i => i.GameSessionId == gameSessionId);
            var result = count > 0;
            return await Task.FromResult(result);

        }

        public async Task<bool> CreateTiles(Tile[] tiles)
        {

            if (tiles != null && tiles != Array.Empty<Tile>())
            {
                return await Task.FromResult(false);
            }

            foreach (var tile in tiles)
            {
                cache.Add(tile);
            }
            return true;

        }

    }
}
