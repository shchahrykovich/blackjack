using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Web.Tests.Models
{
    [TestClass]
    public class BlackjackGameRoundModelTests
    {
        [TestMethod]
        public void CreateTest()
        {
            //Act
            BlackjackGameRoundModel model = new BlackjackGameRoundModel(10, 11, "dealer");

            //Assert
            Assert.AreEqual(10, model.PlayerScore);
            Assert.AreEqual(11, model.DealerScore);
            Assert.AreEqual("dealer", model.Result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNegativePlayerScoreFailTest()
        {
            //Act
            BlackjackGameRoundModel model = new BlackjackGameRoundModel(-1, 10, "player");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateNegativeDealerScoreFailTest()
        {
            //Act
            BlackjackGameRoundModel model = new BlackjackGameRoundModel(10, -1, "player");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateEmptyFailTest()
        {
            //Act
            BlackjackGameRoundModel model = new BlackjackGameRoundModel(10, 10, null);
        }
    }
}
