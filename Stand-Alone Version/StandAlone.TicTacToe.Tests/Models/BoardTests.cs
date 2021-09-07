//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace StandAlone.TicTacToe.Tests.Models
//{
//	[TestClass]
//	public class BoardTests
//	{

//		[TestMethod]
//		public void BoardTest()
//		{

//			var board = new BoardEngine();
//			Assert.IsInstanceOfType(board, typeof(BoardEngine));
//			Assert.IsNotNull(board);

//			Assert.IsTrue(board.IsEmpty);
//			Assert.IsTrue(board.Cells.Count() == 9);

//		}

//		[TestMethod]
//		public void ToStringTest()
//		{

//			var defaultEmptyCell = new Tile("A1").GamePiece;
//			var expected = $"  A B C{Environment.NewLine}1 {defaultEmptyCell} {defaultEmptyCell} {defaultEmptyCell}{Environment.NewLine}2 {defaultEmptyCell} {defaultEmptyCell} {defaultEmptyCell}{Environment.NewLine}3 {defaultEmptyCell} {defaultEmptyCell} {defaultEmptyCell}{Environment.NewLine}";
//			var board = new BoardEngine();
//			Assert.IsFalse(string.IsNullOrWhiteSpace(board.ToString()));
//			Assert.AreEqual(expected, board.ToString());
//		}

//	}

//}