using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamer;
using System;

namespace Gamer.Tests;

[TestClass]
public class CheckersTests
{
    private Checkers game;

    [TestInitialize]
    public void Setup()
    {
        game = new Checkers();
    }

    [TestMethod]
    public void TestInitializeBoard()
    {
        game.InitializeBoard();
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                if ((i < 3 || i > 4) && (i + j) % 2 == 1)
                {
                    Assert.AreNotEqual(' ', game.Board[i, j]);
                }
                else
                {
                    Assert.AreEqual(' ', game.Board[i, j]);
                }
            }
        }
    }

    [TestMethod]
    public void TestIsValidMove_ValidMove()
    {
        game.InitializeBoard();
        Assert.IsTrue(game.IsValidMove(2, 1, 3, 2));
    }

    [TestMethod]
    public void TestIsValidMove_InvalidMove()
    {
        game.InitializeBoard();
        Assert.IsFalse(game.IsValidMove(2, 1, 4, 3));
    }

    [TestMethod]
    public void TestMovePiece_ValidMove()
    {
        game.InitializeBoard();
        game.MovePiece(2, 1, 3, 2);
        Assert.AreEqual(' ', game.Board[2, 1]);
        Assert.AreNotEqual(' ', game.Board[3, 2]);
    }

    [TestMethod]
    public void TestMovePiece_InvalidMove()
    {
        game.InitializeBoard();
        game.MovePiece(2, 1, 4, 3);
        Assert.AreNotEqual(' ', game.Board[2, 1]);
        Assert.AreEqual(' ', game.Board[4, 3]);
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
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                if (game.Board[i, j] == 'B')
                {
                    game.Board[i, j] = ' ';
                }
            }
        }
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