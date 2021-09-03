using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Models;

namespace StandAlone.TicTacToe.Tests.Models
{
	[TestClass]
	public class CellTests
	{

		[TestMethod]
		public void CellTest()
		{
			var cell = new Cell("A1");
			Assert.IsInstanceOfType(cell, typeof(Cell));		
			Assert.IsNotNull(cell);

		}

		[TestMethod]
		public void CellAddressTest()
		{

			var expected = "A1";
			var cell = new Cell(expected);
			var actual = cell.Address;
			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void CellIsEmptyTrueTest()
		{

			var expected = "A1";
			var cell = new Cell(expected);
			Assert.IsTrue(cell.IsEmpty);

		}

		[TestMethod]
		public void CellIsEmptyFalseTest()
		{

			var expected = "A Game Piece";
			var cell = new Cell(expected) {GamePiece = expected};
			var actual = cell.GamePiece;
			Assert.AreEqual(expected, actual);

		}


	}

}