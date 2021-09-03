using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Models;

namespace StandAlone.TicTacToe.Tests.Models
{
	[TestClass]
	public class TicTacToePlayersTests
	{

		private readonly TicTacToePlayer player1 = new TicTacToePlayer("Human", "X");
		private readonly TicTacToePlayer player2 = new TicTacToePlayer("Machine", "O", true);

		[TestMethod]
		public void TicTacToePlayersEmptyTest()
		{

			var players = new TicTacToePlayers();
			Assert.IsTrue(players.AllPlayers.Count == 0);
			Assert.IsNull(players.CurrentPlayer);

		}

		[TestMethod]
		public void TicTacToePlayersTwoPlayersTest()
		{

			var players = new TicTacToePlayers(player1, player2);
			Assert.IsTrue(players.AllPlayers.Count == 2);

		}

		[TestMethod]
		public void IncrementPlayerTest()
		{
			var players = new TicTacToePlayers(player1, player2);
			Assert.AreEqual(player1, players.CurrentPlayer);

			players.IncrementPlayer();
			Assert.AreEqual(player2, players.CurrentPlayer);

			players.IncrementPlayer();
			Assert.AreEqual(player1, players.CurrentPlayer);

		}

		[TestMethod]
		public void ByGamePieceTest()
		{

			var players = new TicTacToePlayers(player1, player2);
			Assert.AreEqual(player2, players.ByGamePiece(player2.GamePiece));

		}

	}

}