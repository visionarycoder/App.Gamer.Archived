using System;
using System.Threading.Tasks;

namespace Gamer.Access.Player.Interface
{
	public interface IPlayerAccess
	{
		Task<Player> GetPlayer(Guid playerId);
		Task<Player[]> FindPlayers(Func<Player, bool> filter);
		Task<bool> DeletePlayer(Guid playerId);
		Task<Player> CreatePlayer(Player player);
		Task<Player[]> CreatePlayers(Player[] players);

	}
}