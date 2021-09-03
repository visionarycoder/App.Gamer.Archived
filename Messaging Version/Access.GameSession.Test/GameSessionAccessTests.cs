using Microsoft.VisualStudio.TestTools.UnitTesting;
using Access.GameSession.Service;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Access.GameSession.Interface;
using Util.ServiceMessaging;

namespace Access.GameSession.Service.Tests
{
    [TestClass]
    public class GameSessionAccessTests
    {

        [TestMethod]
        public void CreateGameSessionTest()
        {
            var request = ServiceMessageFactory<CreateGameSessionRequest>.Create();
            var gameSessionAccess = new GameSessionAccess();
            var response = gameSessionAccess.CreateGameSession(request).Result;

            Assert.IsNotNull(response.GameSession);
            Assert.IsNotNull(response.GameSession.Id);
            CollectionAssert.AreEqual(response.GameSession.Players,Array.Empty<Guid>());
            Assert.AreEqual(response.Errors, string.Empty);
        }

        [TestMethod]
        public void RetrieveGameSessionTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ApplyGameSessionChangesTest()
        {
            Assert.Fail();
        }

    }

}