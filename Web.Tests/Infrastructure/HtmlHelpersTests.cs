using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Blackjack.Domain.Game.Cards;
using Blackjack.Domain.Tests.Helpers;
using Blackjack.Web.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blackjack.Web.Tests.Infrastructure
{
    [TestClass]
    public class HtmlHelpersTests
    {
        private readonly FakeCardPack _pack = new FakeCardPack();

        [TestMethod]
        public void CardHeartsFiveTest()
        {
            //Arrange
            Card card = _pack.GetCard(CardSuit.Hearts, CardType.Five);

            //Act
            MvcHtmlString result = HtmlHelpers.Card(null, card);

            //Assert
            Assert.AreEqual(@"<div class=""card"" style=""background-position: -292px -196px;""></div>", result.ToHtmlString());
        }

        [TestMethod]
        public void CardDiamondsAceTest()
        {
            //Arrange
            Card card = _pack.GetCard(CardSuit.Diamonds, CardType.Ace);

            //Act
            MvcHtmlString result = HtmlHelpers.Card(null, card);

            //Assert
            Assert.AreEqual(@"<div class=""card"" style=""background-position: -0px -98px;""></div>", result.ToHtmlString());
        }

        [TestMethod]
        public void CardClubsNineTest()
        {
            //Arrange
            Card card = _pack.GetCard(CardSuit.Clubs, CardType.Nine);

            //Act
            MvcHtmlString result = HtmlHelpers.Card(null, card);

            //Assert
            Assert.AreEqual(@"<div class=""card"" style=""background-position: -585px -0px;""></div>", result.ToHtmlString());
        }

        [TestMethod]
        public void CardSpadesKingTest()
        {
            //Arrange
            Card card = _pack.GetCard(CardSuit.Spades, CardType.King);

            //Act
            MvcHtmlString result = HtmlHelpers.Card(null, card);

            //Assert
            Assert.AreEqual(@"<div class=""card"" style=""background-position: -877px -294px;""></div>", result.ToHtmlString());
        }
    }
}
