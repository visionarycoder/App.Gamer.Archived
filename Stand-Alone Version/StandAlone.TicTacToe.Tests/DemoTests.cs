using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StandAlone.TicTacToe.Tests
{
	[TestClass]
	public class DemoTests
	{

		[TestMethod]
		public void FailingTest()
		{
			//Assert.Fail();
			Assert.IsTrue(true);
		}

		[TestMethod]
		public void PassingTest()
		{
			Assert.IsTrue(true);
		}

	}

}
