//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

//namespace StandAlone.TicTacToe.Tests.Engines
//{
//	[TestClass]
//	public class ValidationEngineTests
//	{

//		private readonly List<string> addresses = new List<string>
//			{
//				"A1",
//				"A2",
//				"B1",
//				"B2"
//			};

//		[TestMethod]
//		public void ValidationEngineBoardTest()
//		{
//			var board = new BoardEngine();
//			var engine = new ValidationEngine(board);
//			Assert.IsInstanceOfType(engine, typeof(ValidationEngine));
//			Assert.IsNotNull(engine);
//		}

//		[TestMethod]
//		public void ValidationEngineAddressesTest()
//		{
			
//			var engine = new ValidationEngine(addresses);
//			Assert.IsInstanceOfType(engine, typeof(ValidationEngine));
//			Assert.IsNotNull(engine);
//		}

//		[TestMethod]
//		public void ValidateUserInputSuccessTest()
//		{
			
//			var engine = new ValidationEngine(addresses);
//			Assert.AreEqual(ValidationResult.Success, engine.ValidateUserInput("A1") );
//			Assert.AreEqual(ValidationResult.Success, engine.ValidateUserInput(" A1"));
//			Assert.AreEqual(ValidationResult.Success, engine.ValidateUserInput("A1 "));
//			Assert.AreEqual(ValidationResult.Success, engine.ValidateUserInput("A1\r\n"));

//		}

//		[TestMethod]
//		public void ValidateUserInputNotFoundTest()
//		{

//			var engine = new ValidationEngine(addresses);
//			var result = engine.ValidateUserInput("C3");
//			Assert.AreNotEqual(ValidationResult.Success, result);
//			Assert.AreEqual(ValidationEngine.AddressNotFoundError, result.ErrorMessage);

//		}

//		public void ValidateUserInputEmptyTest()
//		{


//			var engine = new ValidationEngine(addresses);
//			var result = engine.ValidateUserInput("");
//			Assert.AreNotEqual(ValidationResult.Success, result);
//			Assert.AreEqual(ValidationEngine.NoInputFoundError, result.ErrorMessage);

//		}
//	}

//}