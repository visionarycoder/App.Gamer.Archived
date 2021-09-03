using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Models;

namespace StandAlone.TicTacToe.Tests.Models
{

	[TestClass]
	public class TicTacToePlayerTests
	{

		[TestMethod]
		public void TicTacToePlayerHumanTest()
		{

			var name = "A Name";
			var gamePiece = "A game piece";
			var player = new TicTacToePlayer(name, gamePiece);
			Assert.IsInstanceOfType(player, typeof(Player));
			Assert.IsInstanceOfType(player, typeof(TicTacToePlayer));
			Assert.IsNotNull(player);

			Assert.AreEqual(name, player.Name);
			Assert.AreEqual(gamePiece, player.GamePiece);
			Assert.IsFalse(player.IsMachine);

		}

		[TestMethod]
		public void TicTacToePlayerMachineTest()
		{

			var name = "A Name";
			var gamePiece = "A game piece";
			var player = new TicTacToePlayer(name, gamePiece, true);
			Assert.IsInstanceOfType(player, typeof(Player));
			Assert.IsInstanceOfType(player, typeof(TicTacToePlayer));
			Assert.IsNotNull(player);

			Assert.AreEqual(name, player.Name);
			Assert.AreEqual(gamePiece, player.GamePiece);
			Assert.IsTrue(player.IsMachine);

		}

	}

}