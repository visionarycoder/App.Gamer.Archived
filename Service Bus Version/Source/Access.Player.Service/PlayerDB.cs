using System.Data.Entity;

namespace Gamer.Access.Player.Service
{

	public class PlayerDB : DbContext
	{

		public PlayerDB()
				: base("name=GamerDB")
		{
		}

		public virtual DbSet<Interface.Player> Players { get; set; }

	}

}