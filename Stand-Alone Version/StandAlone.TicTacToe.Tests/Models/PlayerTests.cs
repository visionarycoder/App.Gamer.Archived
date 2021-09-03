using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Models;

namespace StandAlone.TicTacToe.Tests.Models
{
	[TestClass]
	public class PlayerTests
	{

		[TestMethod]
		public void PlayerHumanTest()
		{

			var expected = "A Name";
			var player = new Player(expected);
			Assert.IsInstanceOfType(player, typeof(Player));
			Assert.IsNotNull(player);

			Assert.AreEqual(expected, player.Name);
			Assert.IsFalse(player.IsMachine);

		}

		[TestMethod]
		public void PlayerMachineTest()
		{

			var expected = "A Name";
			var player = new Player(expected, true);
			Assert.IsInstanceOfType(player, typeof(Player));
			Assert.IsNotNull(player);

			Assert.AreEqual(expected, player.Name);
			Assert.IsTrue(player.IsMachine);

		}

	}

}