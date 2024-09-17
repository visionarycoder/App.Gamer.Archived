using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gamer;
using System;

namespace Gamer.Tests;

[TestClass]
public class TicTacToeTests
{
    private TicTacToe game;

    [TestInitialize]
    public void Setup()
    {
        game = new TicTacToe();
    }

    [TestMethod]
    public void TestInitializeBoard()
    {
        game.InitializeBoard();
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Assert.AreEqual(' ', game.Board[i, j]);
            }
        }
    }

    [TestMethod]
    public void TestIsValidMove_ValidMove()
    {
        game.InitializeBoard();
        Assert.IsTrue(game.IsValidMove(0, 0));
    }

    [TestMethod]
    public void TestIsValidMove_InvalidMove()
    {
        game.InitializeBoard();
        game.Board[0, 0] = 'X';
        Assert.IsFalse(game.IsValidMove(0, 0));
    }

    [TestMethod]
    public void TestCheckWin_NoWin()
    {
        game.InitializeBoard();
        Assert.IsFalse(game.CheckWin());
    }

    [TestMethod]
    public void TestCheckWin_RowWin()
    {
        game.InitializeBoard();
        game.Board[0, 0] = 'X';
        game.Board[0, 1] = 'X';
        game.Board[0, 2] = 'X';
        game.currentPlayer = 'X';
        Assert.IsTrue(game.CheckWin());
    }

    [TestMethod]
    public void TestCheckWin_ColumnWin()
    {
        game.InitializeBoard();
        game.Board[0, 0] = 'X';
        game.Board[1, 0] = 'X';
        game.Board[2, 0] = 'X';
        game.currentPlayer = 'X';
        Assert.IsTrue(game.CheckWin());
    }

    [TestMethod]
    public void TestCheckWin_DiagonalWin()
    {
        game.InitializeBoard();
        game.Board[0, 0] = 'X';
        game.Board[1, 1] = 'X';
        game.Board[2, 2] = 'X';
        game.currentPlayer = 'X';
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
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                game.Board[i, j] = 'X';
            }
        }
        Assert.IsTrue(game.IsBoardFull());
    }

    [TestMethod]
    public void TestAutomatedPlayerMove()
    {
        game.InitializeBoard();
        game.currentPlayer = 'O';
        game.AutomatedPlayerMove();
        bool moveMade = false;
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                if (game.Board[i, j] == 'O')
                {
                    moveMade = true;
                    break;
                }
            }
        }
        Assert.IsTrue(moveMade);
    }
}