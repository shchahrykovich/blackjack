using System;
using System.Collections.Generic;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Blackjack.Domain.Tests.Game.Cards
{
    [TestClass]
    public class ScoreCalculatorTests
    {
        private readonly FakeCardPack _pack = new FakeCardPack();

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetScoreFailTest()
        {
            //Act
            int score = ScoreCalculator.GetScore(null);
        }

        [TestMethod]
        public void GetScoreSimpleTest()
        {
            //Arrange
            IEnumerable<Card> hand = new List<Card>
                                         {
                                             _pack.GetCard(CardSuit.Clubs, CardType.Eight)
                                         };

            //Act
            int score = ScoreCalculator.GetScore(hand);

            //Assert
            Assert.AreEqual(8, score);
        }

        [TestMethod]
        public void GetScoreComplexTest()
        {
            //Arrange
            IEnumerable<Card> hand = new List<Card>
                                         {
                                             _pack.GetCard(CardSuit.Clubs, CardType.Ten),
                                             _pack.GetCard(CardSuit.Clubs, CardType.Four),
                                             _pack.GetCard(CardSuit.Clubs, CardType.Two),
                                         };

            //Act
            int score = ScoreCalculator.GetScore(hand);

            //Assert
            Assert.AreEqual(16, score);
        }

        [TestMethod]
        public void GetScoreOverflowTest()
        {
            //Arrange
            IEnumerable<Card> hand = new List<Card>
                                         {
                                             _pack.GetCard(CardSuit.Clubs, CardType.Ten),
                                             _pack.GetCard(CardSuit.Clubs, CardType.King),
                                             _pack.GetCard(CardSuit.Clubs, CardType.Two),
                                         };

            //Act
            int score = ScoreCalculator.GetScore(hand);

            //Assert
            Assert.AreEqual(22, score);
        }

        [TestMethod]
        public void GetScoreAceMaxTest()
        {
            //Arrange
            IEnumerable<Card> hand = new List<Card>
                                         {
                                             _pack.GetCard(CardSuit.Clubs, CardType.Ace),
                                             _pack.GetCard(CardSuit.Clubs, CardType.King),
                                         };

            //Act
            int score = ScoreCalculator.GetScore(hand);

            //Assert
            Assert.AreEqual(21, score);
        }

        [TestMethod]
        public void GetScoreAceMinTest()
        {
            //Arrange
            IEnumerable<Card> hand = new List<Card>
                                         {
                                             _pack.GetCard(CardSuit.Clubs, CardType.Ace),
                                             _pack.GetCard(CardSuit.Clubs, CardType.King),
                                             _pack.GetCard(CardSuit.Clubs, CardType.Jack),
                                         };

            //Act
            int score = ScoreCalculator.GetScore(hand);

            //Assert
            Assert.AreEqual(21, score);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetScoreEmptyHandTest()
        {
            //Act
            int score = ScoreCalculator.GetScore(null);

            //Assert
            Assert.AreEqual(0, score);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsBlackjackFailTest()
        {
            //Act
            ScoreCalculator.IsBlackjack(null);
        }

        [TestMethod]
        public void NotBlackjackTest()
        {
            //Act
            bool first = ScoreCalculator.IsBlackjack(new[]
                                                          {
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Ace),
                                                              _pack.GetCard(CardSuit.Clubs, CardType.King),
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Jack)
                                                          });
            bool second = ScoreCalculator.IsBlackjack(new[]
                                                          {
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Ace),
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Seven)
                                                          });
            bool third = ScoreCalculator.IsBlackjack(new[]
                                                          {
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Two),
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Five)
                                                          });
            
            //Assert
            Assert.IsFalse(first);
            Assert.IsFalse(second);
            Assert.IsFalse(third);
        }

        [TestMethod]
        public void BlackjackTest()
        {
            //Act
            bool first = ScoreCalculator.IsBlackjack(new[]
                                                          {
                                                              _pack.GetCard(CardSuit.Hearts, CardType.Ace),
                                                              _pack.GetCard(CardSuit.Clubs, CardType.King),
                                                          });
            bool second = ScoreCalculator.IsBlackjack(new[]
                                                          {
                                                              _pack.GetCard(CardSuit.Diamonds, CardType.Ace),
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Queen)
                                                          });
            bool third = ScoreCalculator.IsBlackjack(new[]
                                                          {
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Ace),
                                                              _pack.GetCard(CardSuit.Spades, CardType.Ten)
                                                          });
            bool four = ScoreCalculator.IsBlackjack(new[]
                                                          {
                                                              _pack.GetCard(CardSuit.Clubs, CardType.Ace),
                                                              _pack.GetCard(CardSuit.Spades, CardType.Jack)
                                                          });

            //Assert
            Assert.IsTrue(first);
            Assert.IsTrue(second);
            Assert.IsTrue(third);
            Assert.IsTrue(four);
        }
    }
}
