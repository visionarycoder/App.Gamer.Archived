using System;
using System.Linq;
using System.Threading.Tasks;
using Gamer.StandAlone.Components.Access.Tile;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gamer.StandAlone.Components.Engine.GamePlay
{
    public class GamePlayEngine : IGamePlayEngine
    {

        private readonly ITileAccess tileAccess;

        public GamePlayEngine(IHost host)
            : this (ActivatorUtilities.CreateInstance<ITileAccess>(host.Services))
        {
        }

        public GamePlayEngine(ITileAccess tileAccess)
        {
            this.tileAccess = tileAccess;
        }

		public async Task<bool> IsPlayable(Guid gameSessionId)
		{

            var tiles = await tileAccess.FindTiles(gameSessionId);
			if ( tiles.All(i => i.IsEmpty) )
				return true;

			if ( tiles.All(i => ! i.IsEmpty) )
				return false;

			// Check all possible vectors
            var dictionary = tiles.ToDictionary(tile => tile.Address, tile => tile);

            // A Col
			if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
				return false;

			// B Col
            if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
				return false;

			// C Col
            if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
				return false;

			// 1 Row
            if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
				return false;

			// 2 Row
            if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
				return false;

			// 3 Row
            if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
				return false;

			// Right Diagonal
            if (IsWinningVector(dictionary["A1"], dictionary["B2"], dictionary["C3"]))
				return false;

			// Left Diagonal
            if (IsWinningVector(dictionary["A3"], dictionary["B2"], dictionary["C1"]))
				return false;

			return true;

		}

		public async Task<Guid> FindWinner(Guid gameSessionId)
		{

            if (await IsPlayable(gameSessionId))
            {
                throw new ApplicationException("Game is still playable.");
            }

            var tiles = await tileAccess.FindTiles(gameSessionId);
            var dictionary = tiles.ToDictionary(tile => tile.Address, tile => tile);
            var playerId = Guid.Empty;

            // Check all possible vectors
            // A Col
            if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
                playerId = dictionary["A1"].PlayerId;

            // B Col
            if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
                playerId = dictionary["B1"].PlayerId;

            // C Col
            if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
                playerId = dictionary["C1"].PlayerId;

            // 1 Row
            if (IsWinningVector(dictionary["A1"], dictionary["A2"], dictionary["A3"]))
                playerId = dictionary["A1"].PlayerId;

            // 2 Row
            if (IsWinningVector(dictionary["B1"], dictionary["B2"], dictionary["B3"]))
                playerId = dictionary["B1"].PlayerId;

            // 3 Row
            if (IsWinningVector(dictionary["C1"], dictionary["C2"], dictionary["C3"]))
                playerId = dictionary["C1"].PlayerId;

            // Right Diagonal
            if (IsWinningVector(dictionary["A1"], dictionary["B2"], dictionary["C3"]))
                playerId = dictionary["A1"].PlayerId;

            // Left Diagonal
            if (IsWinningVector(dictionary["A3"], dictionary["B2"], dictionary["C1"]))
                playerId = dictionary["A3"].PlayerId;

            return playerId;

        }

        private static bool IsWinningVector(params Tile[] vectors)
        {
            var first = vectors.First().PlayerId;
            var results = vectors.All(i => i.PlayerId == first);
			return results;
		}

		public async Task<bool> IsTileOpen(Guid gameSessionId, string address)
        {
            
            var tiles = await tileAccess.FindTiles(gameSessionId);
            return tiles.First(i => i.Address == address).IsEmpty;

        }

	}
}