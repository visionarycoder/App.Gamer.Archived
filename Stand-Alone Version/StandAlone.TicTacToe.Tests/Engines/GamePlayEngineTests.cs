using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StandAlone.TicTacToe.Tests.Engines
{
	//[TestClass]
	//public class GamePlayEngineTests
	//{
	//	[TestMethod]
	//	public void GamePlayEngineTest()
	//	{
	//		var board = new BoardEngine();
	//		var engine = new GamePlayEngine(board);
	//		Assert.IsInstanceOfType(engine, typeof(GamePlayEngine));
	//		Assert.IsNotNull(engine);
	//	}

	//	[TestMethod]
	//	public void AutoPlayTest()
	//	{

	//		var board = new BoardEngine();
	//		var engine = new GamePlayEngine(board);
	//		// Top Row
	//		board.A1.GamePiece = "X";
	//		board.A2.GamePiece = "O";
	//		board.A3.GamePiece = "X";
	//		// Middle Row
	//		board.B1.GamePiece = "O";
	//		board.B2.GamePiece = "X";
	//		board.B3.GamePiece = "O";

	//		// Bottom Row
	//		board.C1.GamePiece = "O";
	//		board.C2.GamePiece = "X";
	//		// Only slot left is C3

	//		var actual = engine.AutoPlay();
	//		Assert.AreEqual("C3", actual);

	//	}

	//	[TestMethod]
	//	public void IsPlayableTrueTest()
	//	{
	//		var board = new BoardEngine();
	//		var engine = new GamePlayEngine(board);
	//		Assert.IsTrue(engine.IsPlayable());

	//	}

	//	[TestMethod]
	//	public void IsPlayableFalseTest()
	//	{

	//		var board = new BoardEngine();
	//		var engine = new GamePlayEngine(board);
	//		Assert.IsTrue(engine.IsPlayable());
			
	//		board.A1.GamePiece = "X";
	//		board.A2.GamePiece = "X";
	//		board.A3.GamePiece = "X";

	//		Assert.IsFalse(engine.IsPlayable());


	//	}

	//	[TestMethod]
	//	public void FindWinnerGamePieceTest()
	//	{
	//		var board = new BoardEngine();
	//		var engine = new GamePlayEngine(board);

	//		Assert.IsTrue(engine.IsPlayable());

	//		board.A1.GamePiece = "X";
	//		board.A2.GamePiece = "X";
	//		board.A3.GamePiece = "X";

	//		Assert.IsFalse(engine.IsPlayable());
	//		Assert.AreEqual("X", engine.FindWinnerGamePiece());

	//	}

	//	[TestMethod]
	//	public void IsCellOpenTest()
	//	{
	//		var board = new BoardEngine();
	//		var engine = new GamePlayEngine(board);

	//		board.A1.GamePiece = "X";

	//		Assert.IsFalse(engine.IsCellOpen("A1"));
	//		Assert.IsTrue(engine.IsCellOpen("A2"));
	//		Assert.IsTrue(engine.IsCellOpen("A3"));
	//		Assert.IsTrue(engine.IsCellOpen("B1"));
	//		Assert.IsTrue(engine.IsCellOpen("B2"));
	//		Assert.IsTrue(engine.IsCellOpen("B3"));
	//		Assert.IsTrue(engine.IsCellOpen("C1"));
	//		Assert.IsTrue(engine.IsCellOpen("C2"));
	//		Assert.IsTrue(engine.IsCellOpen("C3"));

	//	}

	//}

}