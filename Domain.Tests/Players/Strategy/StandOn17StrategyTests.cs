using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Players.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Domain.Tests.Players.Strategy
{
    [TestClass]
    public class StandOn17StrategyTests
    {
        [TestMethod]
        public void CreateTest()
        {
            //Act
            StandOn17Strategy strategy = new StandOn17Strategy();
        }

        [TestMethod]
        public void ShouldGetCardTest()
        {
            //Arrange
            StandOn17Strategy strategy = new StandOn17Strategy();

            //Act
            bool result = strategy.ShouldTakeCard(10, null, null);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ShouldNotGetCardTest()
        {
            //Arrange
            StandOn17Strategy strategy = new StandOn17Strategy();

            //Act
            bool result = strategy.ShouldTakeCard(17, null, null);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
