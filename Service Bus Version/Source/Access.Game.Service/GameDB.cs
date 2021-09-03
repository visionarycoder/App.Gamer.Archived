using System.Data.Entity;

namespace Gamer.Access.Game.Service
{
	public class GameDB : DbContext
	{

		public GameDB()
			: base("name=GamerDB")
		{
		}

		public virtual DbSet<Interface.Game> Games { get; set; }

	}
}
