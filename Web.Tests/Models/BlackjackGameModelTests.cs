using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using Blackjack.Web.Models;
using Blackjack.Web.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Web.Tests.Models
{
    [TestClass]
    public class BlackjackGameModelTests
    {
        [TestMethod]
        public void CreateTest()
        {
            //Arrange
            FakeBlackjackGame game = new FakeBlackjackGame();
            Player player = new ComputerPlayer();
            BlackjackStatisticModel statistic = new BlackjackStatisticModel();

            //Act
            BlackjackGameModel model = new BlackjackGameModel(game, player, statistic);

            //Assert
            Assert.AreEqual("?", model.DealerScore);
            Assert.IsNotNull(model.DealerHand);
            Assert.AreEqual(1, model.DealerHand.Count());
            Card dealerCard = model.DealerHand.First();
            Assert.IsTrue(dealerCard.Type == CardType.Seven && dealerCard.Suit == CardSuit.Hearts);
            Assert.AreEqual("0", model.PlayerScore);
            Assert.IsNotNull(model.PlayerHand);
            Assert.AreEqual(0, model.PlayerHand.Count());
            Assert.IsFalse(model.ShowScore);
            Assert.IsTrue(model.ShowDealerBlankCard);
        }

        [TestMethod]
        public void CreateForFinishedGameTest()
        {
            //Arrange
            FakeBlackjackGame game = new FakeBlackjackGame();
            game.IsFinished = true;
            Player player = new ComputerPlayer();
            BlackjackStatisticModel statistic = new BlackjackStatisticModel();

            //Act
            BlackjackGameModel model = new BlackjackGameModel(game, player, statistic);

            //Assert
            Assert.AreEqual("15", model.DealerScore);
            Assert.IsNotNull(model.DealerHand);
            Assert.AreEqual(1, model.DealerHand.Count());
            Card dealerCard = model.DealerHand.First();
            Assert.IsTrue(dealerCard.Type == CardType.Seven && dealerCard.Suit == CardSuit.Hearts);
            Assert.AreEqual("0", model.PlayerScore);
            Assert.IsNotNull(model.PlayerHand);
            Assert.AreEqual(0, model.PlayerHand.Count());
            Assert.IsFalse(model.ShowScore);
            Assert.IsFalse(model.ShowDealerBlankCard);
        }
    }
}
