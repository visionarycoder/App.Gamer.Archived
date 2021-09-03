using System.Data.Entity;

namespace Gamer.Access.Tile.Service
{

	public class TileDB : DbContext
	{

		public TileDB()
				: base("name=GamerDB")
		{
		}

		public virtual DbSet<Interface.Tile> Tiles { get; set; }

	}

}