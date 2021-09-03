using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Models;

namespace StandAlone.TicTacToe.Tests.Models
{
	[TestClass]
	public class BoardTests
	{

		[TestMethod]
		public void BoardTest()
		{

			var board = new Board();
			Assert.IsInstanceOfType(board, typeof(Board));
			Assert.IsNotNull(board);

			Assert.IsTrue(board.IsEmpty);
			Assert.IsTrue(board.Cells.Count() == 9);

		}

		[TestMethod]
		public void ToStringTest()
		{

			var defaultEmptyCell = new Cell("A1").GamePiece;
			var expected = $"  A B C{Environment.NewLine}1 {defaultEmptyCell} {defaultEmptyCell} {defaultEmptyCell}{Environment.NewLine}2 {defaultEmptyCell} {defaultEmptyCell} {defaultEmptyCell}{Environment.NewLine}3 {defaultEmptyCell} {defaultEmptyCell} {defaultEmptyCell}{Environment.NewLine}";
			var board = new Board();
			Assert.IsFalse(string.IsNullOrWhiteSpace(board.ToString()));
			Assert.AreEqual(expected, board.ToString());
		}

	}

}