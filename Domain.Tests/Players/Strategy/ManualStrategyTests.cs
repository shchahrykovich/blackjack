using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blackjack.Domain.Players.Strategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Blackjack.Domain.Tests.Players.Strategy
{
    [TestClass]
    public class ManualStrategyTests
    {
        [TestMethod]
        public void CreateTest()
        {
            //Act
            ManualStrategy strategy = new ManualStrategy();
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ShouldTakeCardTest()
        {
            //Arrange
            ManualStrategy strategy = new ManualStrategy();

            //Act
            strategy.ShouldTakeCard(0, null, null);
        }
    }
}
