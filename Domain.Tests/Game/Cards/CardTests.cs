using System.Collections.Generic;
using System.Linq;
using Blackjack.Domain.Game;
using Blackjack.Domain.Game.Cards;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Blackjack.Domain.Tests.Game.Cards
{
    [TestClass]
    public class CardTests
    {                   
        [TestMethod]
        public void GetNewPackTest()
        {
            //Act
            CardPack pack = Card.GetNewPack();

            //Assert
            Assert.AreEqual(52, pack.Count());
            for(int suitIndex = 0; suitIndex < 4; suitIndex ++)
            {
                for (int cardIndex = 0; cardIndex < 13; cardIndex++)
                {
                    CardType type = (CardType) cardIndex + 1;
                    CardSuit suit = (CardSuit) suitIndex + 1;

                    IEnumerable<Card> card = pack.Where(c => c.Suit == suit && c.Type == type);
                    Assert.AreEqual(1, card.Count());
                }
            }
        }

        [TestMethod]
        public void GetNewPackUniquenessTest()
        {
            //Act
            CardPack firstPack = Card.GetNewPack();
            CardPack secondPack = Card.GetNewPack();

            //Assert
            Assert.AreNotEqual(firstPack, secondPack);            
        }
    }
}
