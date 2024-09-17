using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gamer.Client.ConsoleApp.Data;

namespace Gamer.Client.ConsoleApp.Access.Player
{
    public class PlayerAccess : IPlayerAccess
    {

        private readonly GamerDbContext db;

        public PlayerAccess(GamerDbContext db)
        {
            this.db = db;
        }

        public async Task<Interface.Player> GetPlayer(Guid playerId)
        {
            var player = await db.Players.SingleOrDefaultAsync(i => i.Id == playerId);
            return player?.Convert();
        }

        public async Task<List<Interface.Player>> FindPlayers(FindPlayersFilter filter)
        {

            var query = db.Players.AsNoTracking().AsQueryable();
            if (!filter.PlayerIds.Any())
            {
                foreach (var playerId in filter.PlayerIds)
                {
                    query = query.Where(i => i.Id == playerId);
                }
            }

            var results = await query.ToListAsync();
            return results.Select(i => i.Convert()).ToList();

        }

        public async Task<Interface.Player> CreatePlayer(Interface.Player player)
        {

            var entity = await db.Players.SingleOrDefaultAsync(i => i.Id == player.Id);
            if (entity != null)
                return entity.Convert();

            entity = new Resource.DataSource.Model.Player
            {
                GamePieceIds = player.GamePieceIds,
                Id = player.Id,
                IsMachine = player.IsMachine,
                Name = player.Name,
            };
            var entityEntry = await db.Players.AddAsync(entity);
            player = entityEntry.Entity.Convert();
            return player;

        }

        public async Task<List<Interface.Player>> CreatePlayers(List<Interface.Player> players)
        {

            var dbObjects = players.Select(i => i.Convert());
            await db.Players.AddRangeAsync(dbObjects);
            await db.SaveChangesAsync();

            var newPlayers = new List<Interface.Player>();
            foreach (var player in players)
            {
                var dbObject = await db.Players.SingleOrDefaultAsync(i => i.Id == player.Id);
                if (dbObject == null)
                    continue;
                newPlayers.Add(dbObject.Convert());
            }
            return newPlayers;

        }

        public async Task<bool> RemovePlayer(Interface.Player player)
        {
            var dbObject = await db.Players.SingleOrDefaultAsync(i => i.Id == player.Id);
            if (dbObject == null)
                return true;
            db.Players.Remove(dbObject);
            var count = await db.SaveChangesAsync();
            return count == 1;
        }

        public async Task<bool> RemovePlayers(List<Interface.Player> players)
        {
            var result = true;
            foreach (var player in players)
            {
                result &= await RemovePlayer(player);
            }
            return result;
        }

    }
