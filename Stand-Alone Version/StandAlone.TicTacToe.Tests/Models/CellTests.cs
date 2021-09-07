using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StandAlone.TicTacToe.Tests.Models
{
	[TestClass]
	public class CellTests
	{

		[TestMethod]
		public void CellTest()
		{
			var cell = new Tile("A1");
			Assert.IsInstanceOfType(cell, typeof(Tile));		
			Assert.IsNotNull(cell);

		}

		[TestMethod]
		public void CellAddressTest()
		{

			var expected = "A1";
			var cell = new Tile(expected);
			var actual = cell.Address;
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void CellIsEmptyTrueTest()
		{

			var expected = "A1";
			var cell = new Tile(expected);
			Assert.IsTrue(cell.IsEmpty);

		}

		[TestMethod]
		public void CellIsEmptyFalseTest()
		{

			var expected = "A Game Piece";
			var cell = new Tile(expected) {GamePiece = expected};
			var actual = cell.GamePiece;
			Assert.AreEqual(expected, actual);

		}


	}

}