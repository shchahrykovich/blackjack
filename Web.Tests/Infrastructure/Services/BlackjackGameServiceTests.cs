using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain;
using Blackjack.Domain.Players;
using Blackjack.Web.Infrastructure.Services;
using Blackjack.Web.Models;
using Blackjack.Web.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Web.Tests.Infrastructure.Services
{
    [TestClass]
    public class BlackjackGameServiceTests
    {
        [TestMethod]
        public void CreateTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();

            //Act
            BlackjackGameService service = new BlackjackGameService(session);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateFailTest()
        {
            //Act
            BlackjackGameService service = new BlackjackGameService(null);
        }

        [TestMethod]
        public void GetCurrentPlayerFromCacheTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();
            HumanPlayer player = new HumanPlayer();
            session.Storage.Add("Player", player);
            BlackjackGameService service = new BlackjackGameService(session);

            //Act
            HumanPlayer result = service.GetCurrentPlayer();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result == player);
        }

        [TestMethod]
        public void GetCurrentPlayerTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();
            BlackjackGameService service = new BlackjackGameService(session);

            //Act
            HumanPlayer result = service.GetCurrentPlayer();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCurrentGameFromCacheTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();
            FakeBlackjackGame game = new FakeBlackjackGame();
            session.Storage.Add("Blackajck", game);
            BlackjackGameService service = new BlackjackGameService(session);

            //Act
            IBlackjackGame result = service.GetCurrentGame();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, game);
        }

        [TestMethod]
        public void GetCurrentGameTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();
            BlackjackGameService service = new BlackjackGameService(session);

            //Act
            IBlackjackGame result = service.GetCurrentGame();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NewGameTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();
            session.Storage.Add("Player", new object());
            session.Storage.Add("Blackajck", new object());
            BlackjackGameService service = new BlackjackGameService(session);

            //Act
            service.NewGame();

            //Assert
            Assert.IsNull(session.Storage["Player"]);
            Assert.IsNull(session.Storage["Blackajck"]);
        }

        [TestMethod]
        public void GetStatisticFromCacheTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();
            BlackjackStatisticModel statistic = new BlackjackStatisticModel();
            session.Storage.Add("Statistic", statistic);
            BlackjackGameService service = new BlackjackGameService(session);

            //Act
            BlackjackStatisticModel result = service.GetStatistic();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, statistic);
        }

        [TestMethod]
        public void GetStatisticTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();
            BlackjackGameService service = new BlackjackGameService(session);

            //Act
            BlackjackStatisticModel result = service.GetStatistic();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SaveResultsTest()
        {
            //Arrange
            FakeHttpSessionStateBase session = new FakeHttpSessionStateBase();
            BlackjackGameService service = new BlackjackGameService(session);
            FakeBlackjackGameResult fakeResult = new FakeBlackjackGameResult();

            //Act
            service.SaveResults(fakeResult);

            //Assert
            BlackjackStatisticModel statistic = session.Storage["Statistic"] as BlackjackStatisticModel;
            Assert.IsNotNull(statistic);
            Assert.AreEqual(1, statistic.DrawsCount);
        }
    }
}
