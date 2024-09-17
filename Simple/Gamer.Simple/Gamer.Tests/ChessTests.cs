using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamer;
using System;

namespace Gamer.Tests;

[TestClass]
public class ChessTests
{
    private Chess game;

    [TestInitialize]
    public void Setup()
    {
        game = new Chess();
    }

    [TestMethod]
    public void TestInitializeBoard()
    {
        game.InitializeBoard();
        // Check if the initial positions of the pieces are correct
        Assert.AreEqual('R', game.Board[0, 0]);
        Assert.AreEqual('N', game.Board[0, 1]);
        Assert.AreEqual('B', game.Board[0, 2]);
        Assert.AreEqual('Q', game.Board[0, 3]);
        Assert.AreEqual('K', game.Board[0, 4]);
        Assert.AreEqual('B', game.Board[0, 5]);
        Assert.AreEqual('N', game.Board[0, 6]);
        Assert.AreEqual('R', game.Board[0, 7]);
        for (var i = 0; i < 8; i++)
        {
            Assert.AreEqual('P', game.Board[1, i]);
            Assert.AreEqual('p', game.Board[6, i]);
        }
        Assert.AreEqual('r', game.Board[7, 0]);
        Assert.AreEqual('n', game.Board[7, 1]);
        Assert.AreEqual('b', game.Board[7, 2]);
        Assert.AreEqual('q', game.Board[7, 3]);
        Assert.AreEqual('k', game.Board[7, 4]);
        Assert.AreEqual('b', game.Board[7, 5]);
        Assert.AreEqual('n', game.Board[7, 6]);
        Assert.AreEqual('r', game.Board[7, 7]);
    }

    [TestMethod]
    public void TestIsValidMove_ValidMove()
    {
        game.InitializeBoard();
        Assert.IsTrue(game.IsValidMove(1, 0, 3, 0)); // Pawn move
    }

    [TestMethod]
    public void TestIsValidMove_InvalidMove()
    {
        game.InitializeBoard();
        Assert.IsFalse(game.IsValidMove(0, 0, 2, 0)); // Invalid Rook move
    }

    [TestMethod]
    public void TestMovePiece_ValidMove()
    {
        game.InitializeBoard();
        game.MovePiece(1, 0, 3, 0); // Pawn move
        Assert.AreEqual(' ', game.Board[1, 0]);
        Assert.AreEqual('P', game.Board[3, 0]);
    }

    [TestMethod]
    public void TestMovePiece_InvalidMove()
    {
        game.InitializeBoard();
        game.MovePiece(0, 0, 2, 0); // Invalid Rook move
        Assert.AreEqual('R', game.Board[0, 0]);
        Assert.AreEqual(' ', game.Board[2, 0]);
    }

    [TestMethod]
    public void TestCheckWin_NoWin()
    {
        game.InitializeBoard();
        Assert.IsFalse(game.CheckWin());
    }

    [TestMethod]
    public void TestCheckWin_Win()
    {
        game.InitializeBoard();
        // Simulate a win condition by removing the black king
        game.Board[7, 4] = ' ';
        Assert.IsTrue(game.CheckWin());
    }

    [TestMethod]
    public void TestIsBoardFull_NotFull()
    {
        game.InitializeBoard();
        Assert.IsFalse(game.IsBoardFull());
    }

    [TestMethod]
    public void TestIsBoardFull_Full()
    {
        game.InitializeBoard();
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                game.Board[i, j] = 'X';
            }
        }
        Assert.IsTrue(game.IsBoardFull());
    }
}