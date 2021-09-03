using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Managers;

namespace StandAlone.TicTacToe.Tests.Managers
{
	[TestClass]
	public class GameManagerTests
	{
		[TestMethod]
		public void GameManagerTest()
		{

			var gameManager = new GameManager();
			Assert.IsInstanceOfType(gameManager, typeof(GameManager));
			Assert.IsNotNull(gameManager);
		}

		//[TestMethod]
		//public void PlayTest()
		//{
				// Can't actually test this because it has I/O
		//	var gameManager = new GameManager();
		//	gameManager.Play();
		//}

	}

}