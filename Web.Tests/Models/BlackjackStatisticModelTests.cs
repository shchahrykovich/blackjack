using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Web.Tests.Models
{
    [TestClass]
    public class BlackjackStatisticModelTests
    {
        [TestMethod]
        public void CreateTest()
        {

            //Act
            BlackjackStatisticModel model = new BlackjackStatisticModel();
 
            //Assert
            Assert.AreEqual(0, model.DrawsCount);
            Assert.AreEqual("0 %", model.DrawsPercent);
            Assert.AreEqual(0, model.WinsCount);
            Assert.AreEqual("0 %", model.WinsPercent);
            Assert.AreEqual(0, model.LoosesCount);
            Assert.AreEqual("0 %", model.LoosesPercent);
            Assert.IsNotNull(model.Rounds);
            Assert.AreEqual(0, model.Rounds.Count());
        }

        [TestMethod]
        public void AddDrawRoundTest()
        {
            //Arrange
            BlackjackStatisticModel model = new BlackjackStatisticModel();

            //Act
            model.AddDrawRound(10, 10);

            //Assert
            Assert.AreEqual(1, model.DrawsCount);
            Assert.AreEqual("100 %", model.DrawsPercent);
            Assert.AreEqual(0, model.WinsCount);
            Assert.AreEqual("0 %", model.WinsPercent);
            Assert.AreEqual(0, model.LoosesCount);
            Assert.AreEqual("0 %", model.LoosesPercent);
            Assert.IsNotNull(model.Rounds);
            Assert.AreEqual(1, model.Rounds.Count());
            Assert.AreEqual(10, model.Rounds.ElementAt(0).DealerScore);
            Assert.AreEqual(10, model.Rounds.ElementAt(0).PlayerScore);
            Assert.AreEqual("Draw", model.Rounds.ElementAt(0).Result);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddDrawRoundFailTest()
        {
            //Arrange
            BlackjackStatisticModel model = new BlackjackStatisticModel();

            //Act
            model.AddDrawRound(11, 10);
        }

        [TestMethod]
        public void AddPlayerRoundTest()
        {
            //Arrange
            BlackjackStatisticModel model = new BlackjackStatisticModel();

            //Act
            model.AddPlayerRound(12, 10);

            //Assert
            Assert.AreEqual(0, model.DrawsCount);
            Assert.AreEqual("0 %", model.DrawsPercent);
            Assert.AreEqual(1, model.WinsCount);
            Assert.AreEqual("100 %", model.WinsPercent);
            Assert.AreEqual(0, model.LoosesCount);
            Assert.AreEqual("0 %", model.LoosesPercent);
            Assert.IsNotNull(model.Rounds);
            Assert.AreEqual(1, model.Rounds.Count());
            Assert.AreEqual(10, model.Rounds.ElementAt(0).DealerScore);
            Assert.AreEqual(12, model.Rounds.ElementAt(0).PlayerScore);
            Assert.AreEqual("You win!", model.Rounds.ElementAt(0).Result);
        }

        [TestMethod]
        public void AddDealerRoundTest()
        {
            //Arrange
            BlackjackStatisticModel model = new BlackjackStatisticModel();

            //Act
            model.AddDealerRound(12, 14);

            //Assert
            Assert.AreEqual(0, model.DrawsCount);
            Assert.AreEqual("0 %", model.DrawsPercent);
            Assert.AreEqual(0, model.WinsCount);
            Assert.AreEqual("0 %", model.WinsPercent);
            Assert.AreEqual(1, model.LoosesCount);
            Assert.AreEqual("100 %", model.LoosesPercent);
            Assert.IsNotNull(model.Rounds);
            Assert.AreEqual(1, model.Rounds.Count());
            Assert.AreEqual(14, model.Rounds.ElementAt(0).DealerScore);
            Assert.AreEqual(12, model.Rounds.ElementAt(0).PlayerScore);
            Assert.AreEqual("Dealer win", model.Rounds.ElementAt(0).Result);
        }
    }
}
