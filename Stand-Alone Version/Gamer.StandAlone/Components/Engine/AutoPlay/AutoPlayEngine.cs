using System;
using System.Linq;
using System.Threading.Tasks;
using Gamer.StandAlone.Components.Access.Tile;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gamer.StandAlone.Components.Engine.AutoPlay
{

    public class AutoPlayEngine : IAutoPlayEngine
    {

        private readonly Random random = new Random();
        private readonly ITileAccess tileAccess;

        public AutoPlayEngine(IHost host)
        : this(ActivatorUtilities.CreateInstance<ITileAccess>(host.Services))
        {

        }

        public AutoPlayEngine(ITileAccess tileAccess)
        {
            this.tileAccess = tileAccess;
        }

        public async Task<Tile> PlayTurn(Guid gameSessionId, Guid playerId)
        {
        
            // playerId is unused.  Needed for intelligent auto-player.
            // ToDo: Add intelligent auto-player.
            var tiles = await tileAccess.FindTiles(gameSessionId);
            var emptyTiles = tiles.Where(i => i.IsEmpty).ToList();
            var idx = random.Next(0, emptyTiles.Count - 1);
            return emptyTiles[idx];

        }

    }

}
