using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blackjack.Web.Controllers;
using Blackjack.Web.Models;
using Blackjack.Web.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Web.Tests.Controllers
{
    [TestClass]
    public class BlackjackControllerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateFaileTest()
        {
            //Act
            BlackjackController controller = new BlackjackController(null);
        }

        [TestMethod]
        public void CreareTest()
        {
            //Arrange
            FakeBlackjackGameService service = new FakeBlackjackGameService();

            //Act 
            BlackjackController controller = new BlackjackController(service);
        }

        [TestMethod]
        public void IndexTest()
        {
            //Arrange
            FakeBlackjackGameService service = new FakeBlackjackGameService();
            BlackjackController controller = new BlackjackController(service);

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            BlackjackGameModel model = result.Model as BlackjackGameModel;
            Assert.IsNotNull(model);
            Assert.AreEqual("?", model.DealerScore);
            Assert.IsTrue(model.ShowBanner);
            Assert.IsTrue(model.ShowDealerBlankCard);
            Assert.IsFalse(model.ShowScore);
        }

        [TestMethod]
        public void NewGameTest()
        {
            //Arrange
            FakeBlackjackGameService service = new FakeBlackjackGameService();
            BlackjackController controller = new BlackjackController(service);

            //Act
            JsonResult result = controller.NewGame() as JsonResult;

            //Assert
            Assert.IsNotNull(result);
            BlackjackGameJson model = result.Data as BlackjackGameJson;
            Assert.IsNotNull(model);
            Assert.AreEqual("?", model.DealerScore);
            Assert.AreEqual("0 (0 %)", model.DrawsCounter);
            Assert.IsTrue(model.ShowDealerBlankCard);
            Assert.AreEqual("0 (0 %)", model.WinsCounter);
            Assert.AreEqual("0 (0 %)", model.LoosesCounter);
        }

        [TestMethod]
        public void HitTest()
        {
            //Arrange
            FakeBlackjackGameService service = new FakeBlackjackGameService();
            BlackjackController controller = new BlackjackController(service);
            int playerScore = service.GetCurrentPlayer().Score;

            //Act
            JsonResult result = controller.Hit() as JsonResult;

            //Assert
            Assert.IsNotNull(result);
            BlackjackGameJson model = result.Data as BlackjackGameJson;
            Assert.IsNotNull(model);
            Assert.AreEqual("?", model.DealerScore);
            Assert.AreEqual("0 (0 %)", model.DrawsCounter);
            Assert.AreNotEqual(playerScore, model.PlayerScore);
            Assert.IsTrue(model.ShowDealerBlankCard);
            Assert.AreEqual("0 (0 %)", model.WinsCounter);
            Assert.AreEqual("0 (0 %)", model.LoosesCounter);
        }

        [TestMethod]
        public void StandTest()
        {
            //Arrange
            FakeBlackjackGameService service = new FakeBlackjackGameService();
            BlackjackController controller = new BlackjackController(service);

            //Act
            JsonResult result = controller.Stand() as JsonResult;

            //Assert
            Assert.IsNotNull(result);
            BlackjackGameJson model = result.Data as BlackjackGameJson;
            Assert.IsNotNull(model);
            Assert.AreNotEqual("?", model.DealerScore);
            Assert.IsFalse(model.ShowDealerBlankCard);
        }
    }
}
