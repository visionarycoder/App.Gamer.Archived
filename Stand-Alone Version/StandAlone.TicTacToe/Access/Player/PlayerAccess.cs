using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Access.Player
{
    public class PlayerAccess : IPlayerAccess
    {

        private static readonly HashSet<Player> cache = new HashSet<Player>();

        public async Task<Player> GetPlayer(Guid playerId)
        {

            var player = cache.FirstOrDefault(i => i.Id == playerId);
            return await Task.FromResult(player);

        }

        public async Task<Player[]> FindPlayers(Func<Player, bool> filter)
        {

            var players = cache.Where(filter).ToArray();
            return await Task.FromResult(players);

        }

        public async Task<bool> DeletePlayer(Guid playerId)
        {
        
            var count = cache.RemoveWhere(i => i.Id == playerId);
            return await Task.FromResult(count > 0);

        }

        public async Task<Player> CreatePlayer(string name, string gamePiece, bool isMachine = false)
        {

            var player = new Player(name, gamePiece, isMachine);
            cache.Add(player);
            return await Task.FromResult(player);

        }


    }
}
