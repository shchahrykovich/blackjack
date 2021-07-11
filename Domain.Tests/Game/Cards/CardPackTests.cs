using System;
using System.Collections.Generic;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Domain.Tests.Game.Cards
{
    [TestClass]
    public class CardPackTests
    {
        [TestMethod]
        public void CreateTest()
        {
            //Act
            CardPack pack = Card.GetNewPack();

            //Assert
            Assert.IsNotNull(pack);
            Assert.AreEqual(52, pack.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateEmptyCollectionTest()
        {
            //Act
            CardPack pack = new CardPack(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateInvalidCollectionTest()
        {
            //Arrange
            CardPack pack = Card.GetNewPack();
            pack.GetCard();

            //Act
            CardPack result = new CardPack(pack);
        }


        [TestMethod]
        public void CountTest()
        {
            //Arrange
            CardPack pack = Card.GetNewPack();

            //Act & Assert
            const int expected = 52;
            for (int i = 1; i < 5; i++)
            {
                pack.GetCard();
                Assert.AreEqual(expected - i, pack.Count());
            }
        }

        [TestMethod]
        public void ShuffleTest()
        {
            //Arrange
            CardPack pack = Card.GetNewPack();
            List<Card> initial = new List<Card>(pack);

            //Act
            pack.Shuffle();

            //Asert
            bool isEqual = true;
            for (int i = 0; i < 5; i++)
            {
                Card card = pack.GetCard();
                Card initialCard = initial[i];

                bool isCurrentEqual = (card.Suit == initialCard.Suit
                                       && card.Type == initialCard.Type);
                
                isEqual = isEqual && isCurrentEqual;
            }

            Assert.IsFalse(isEqual);
        }
    }
}
