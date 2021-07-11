using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Players;
using Blackjack.Domain.Players.Strategies;
using Blackjack.Domain.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Blackjack.Domain.Tests.Players
{
    [TestClass]
    public class ComputerPlayerTests
    {
        private readonly FakeCardPack _pack = new FakeCardPack();

        [TestMethod]
        public void CreateTest()
        {
            //Act
            ComputerPlayer player = new ComputerPlayer(new StandOn17Strategy());
        }

        [TestMethod]
        public void GetCardTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer(new StandOn17Strategy());
            BlackjackGame game = new BlackjackGame(player, new[] { new HumanPlayer() });
            game.Start();

            //Act
            bool result = player.ShouldTakeCard(game);

            //Assert
            if(player.Score < 17)
            {
                Assert.IsTrue(result);
            }
            else
            {
                Assert.IsFalse(result);    
            }
        }

        [TestMethod]
        public void ScoreTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer();
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Two));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Five));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ace));

            //Act
            int score = player.Score;

            //Assert
            Assert.AreEqual(18, score);
        }

        [TestMethod]
        public void SecondScoreTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer();
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ace));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ten));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.King));

            //Act
            int score = player.Score;

            //Assert
            Assert.AreEqual(21, score);
        }

        [TestMethod]
        public void BlackjackTest()
        { 
            //Arrange
            ComputerPlayer player = new ComputerPlayer();
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ace));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ten));

            //Act
            bool result = player.IsBlackjack;

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotBlackjackTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer();
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ace));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Two));

            //Act
            bool result = player.IsBlackjack;

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BustTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer();
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ten));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ten));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ten));

            //Act
            bool result = player.IsBust;

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotBustTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer();
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ace));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Two));

            //Act
            bool result = player.IsBust;

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HandTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer();
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Ace));
            player.TakeCard(_pack.GetCard(CardSuit.Clubs, CardType.Two));

            //Act
            IEnumerable<Card> result = player.Hand.ToArray();

            //Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsNotNull(result.Where(c => c.Suit == CardSuit.Clubs && c.Type == CardType.Ace).FirstOrDefault());
            Assert.IsNotNull(result.Where(c => c.Suit == CardSuit.Clubs && c.Type == CardType.Two).FirstOrDefault());
        }

        [TestMethod]
        public void FinishedTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer(new NoCardStrategy());
            player.ShouldTakeCard(new FakeBlackjackGame());

            //Act
            bool result = player.IsFinishedTurn;

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotFinishedTest()
        {
            //Arrange
            ComputerPlayer player = new ComputerPlayer(new NoCardStrategy());

            //Act
            bool result = player.IsFinishedTurn;

            //Assert
            Assert.IsFalse(result);
        }
    }
}
